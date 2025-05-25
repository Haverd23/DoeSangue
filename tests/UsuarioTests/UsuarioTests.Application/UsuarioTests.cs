using DOS.Core.Data;
using DOS.Usuario.Application.Commands;
using DOS.Usuario.Application.CommandsHandlers;
using DOS.Usuario.Domain;
using DOS.Usuario.Domain.Enums;
using Moq;

namespace UsuarioTests.Application
{
    public class UsuarioTests
    {
        private readonly Mock<IUsuarioRepository> _mockUserRepository;

        public UsuarioTests()
        {
            _mockUserRepository = new Mock<IUsuarioRepository>();
        }

        [Fact(DisplayName = "Deve criar um usuário e retornar o ID")]
        public async Task HandleAsync_DeveCriarUsuarioERetornarId()
        {
            // Arrange
            var nome = "João";
            var email = "email@gmail.com";
            var cpf = "12345678909";
            var telefone = "11987654321";
            var tipoSanguineo = TipoSanguineo.ABNegativo;

            var command = new UsuarioCriadoCommand(nome, email, cpf, telefone, tipoSanguineo);
            var handler = new UsuarioCriadoCommandHandler(_mockUserRepository.Object);

            _mockUserRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockUserRepository.Verify(x => x.Adcionar(It.IsAny<User>()), Times.Once);
            _mockUserRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }
    }
}

