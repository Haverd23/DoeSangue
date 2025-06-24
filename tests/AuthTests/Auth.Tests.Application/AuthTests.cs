using DOS.Auth.Application.Commands;
using DOS.Auth.Application.CommandsHandlers;
using DOS.Auth.Application.Services;
using DOS.Auth.Application.Services.Interfaces;
using DOS.Auth.Domain.Interfaces;
using DOS.Auth.Domain.Models;
using DOS.Core.Data;
using DOS.Core.DomainObjects;
using Moq;
using System.ComponentModel;

namespace Auth.Tests.Application
{
    [DisplayName("Testes de Autenticação e Criação de Usuário")]
    public class AuthTests
    {
        private readonly Mock<IUserRepository> _mockUser = new();
        private readonly Mock<IDomainEventDispatcher> _mockDispatcher = new();
        private readonly Mock<ISenhaCriptografia> _mockSenha = new();
        private readonly Mock<IUnityOfWork> _mockUnitOfWork = new();

        public AuthTests()
        {
            _mockUnitOfWork.Setup(u => u.Commit()).ReturnsAsync(true);
            _mockUser.Setup(u => u.UnitOfWork).Returns(_mockUnitOfWork.Object);
        }

        [Fact(DisplayName = "Deve criar um usuário e retornar o ID")]
        public async Task HandleAsync_DeveCriarUsuarioERetornarId()
        {
            // Arrange
            var command = new UsuarioCriadoCommand("teste123@gmail.com", "Ssss33t");
            _mockSenha.Setup(s => s.SenhaHash(command.Senha)).Returns("546546546546D54fdsfds");
            var handler = new UsuarioCriadoCommandHandler(_mockUser.Object, _mockDispatcher.Object, _mockSenha.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockUser.Verify(x => x.AdicionarUser(It.IsAny<User>()), Times.Once);
            _mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
            _mockDispatcher.Verify(x => x.DispatchEventsAsync(It.IsAny<IEnumerable<IDomainEvent>>()), Times.Once);
        }

        [Fact(DisplayName = "Deve autenticar usuário e retornar token")]
        public async Task Autenticar_DeveLogarEReceberToken()
        {
            // Arrange
            var user = new User("teste123@gmail.com", "Ssss33t");
            _mockUser.Setup(u => u.ObterPorEmail(user.Email)).ReturnsAsync(user);
            _mockSenha.Setup(s => s.VerificarSenha("Ssss33t", user.Senha)).Returns(true);
            var mockToken = new Mock<ITokenJWT>();
            mockToken.Setup(t => t.GerarToken(user)).ReturnsAsync("fake-token");

            var login = new LoginService(mockToken.Object, _mockSenha.Object, _mockUser.Object);

            // Act
            var token = await login.Autenticar(user.Email, "Ssss33t");

            // Assert
            Assert.Equal("fake-token", token);
            _mockUser.Verify(x => x.ObterPorEmail(It.IsAny<Email>()), Times.Once);
            _mockSenha.Verify(x => x.VerificarSenha(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            mockToken.Verify(x => x.GerarToken(It.IsAny<User>()), Times.Once);
        }
    }
}