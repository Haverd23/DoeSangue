using DOS.Core.Data;
using DOS.Core.DomainObjects;
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
        private readonly Mock<IDomainEventDispatcher> _mockDomainEventDispatcher = new();


        public UsuarioTests()
        {
            _mockUserRepository = new Mock<IUsuarioRepository>();
            _mockDomainEventDispatcher = new Mock<IDomainEventDispatcher>();
        }

        [Fact(DisplayName = "Deve criar um usuário e retornar o ID")]
        public async Task HandleAsync_DeveCriarUsuarioERetornarId()
        {
            // Arrange
            var nome = "João";
            var email = "email@gmail.com";
            var cpf = "12345678909";
            var telefone = "11987654321";
            var tipoSanguineo = "ONegativo";

            var command = new UsuarioCriadoCommand(nome,cpf, telefone, tipoSanguineo);
            var handler = new UsuarioCriadoCommandHandler(
                                _mockUserRepository.Object,
                                _mockDomainEventDispatcher.Object
                            );

            _mockUserRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            _mockUserRepository.Verify(x => x.Adcionar(It.IsAny<User>()), Times.Once);
            _mockUserRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);
        }
    }
}

