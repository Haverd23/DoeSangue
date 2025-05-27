
using DOS.Core.Data;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.CommandsHandlers;
using DOS.Doacao.Domain;
using DOS.Usuario.Domain;
using DOS.Usuario.Domain.Enums;
using Moq;

namespace DoacaoTests.Application
{
    public class CommandsTests
    {
        private readonly Mock<IDoacaoRepository> _mockDoacaoRepository;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly AgendarDoacaoCommandHandler _handler;

        public CommandsTests()
        {
            _mockDoacaoRepository = new Mock<IDoacaoRepository>();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _handler = new AgendarDoacaoCommandHandler(_mockDoacaoRepository.Object, _mockUsuarioRepository.Object);
        }

        [Fact(DisplayName = "Deve criar doação e retornar Id")]
        public async Task HandleAsync_DeveCriarDoacaoERetornarId()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var dataAgendada = DateTime.Now.AddDays(1);
            var tipoSanguineo = TipoSanguineo.APositivo;

            var usuario = new User(
                usuarioId,
                "João Silva",
                "joao.silva@teste.com",
                "00327332085",
                "00327332085",
                tipoSanguineo
            );

            _mockUsuarioRepository
                .Setup(r => r.GetById(usuarioId))
                .ReturnsAsync(usuario);

            _mockDoacaoRepository
                .Setup(r => r.AdicionarAsync(It.IsAny<DoacaoRegistro>()))
                .Returns(Task.CompletedTask);

            _mockDoacaoRepository
                .Setup(r => r.UnitOfWork.Commit())
                .ReturnsAsync(true);

            var command = new AgendarDoacaoCommand(agendaId, dataAgendada);
            command.UserId = usuarioId;

            var handler = new AgendarDoacaoCommandHandler(_mockDoacaoRepository.Object, _mockUsuarioRepository.Object);

            // Act
            var result = await handler.HandleAsync(command);

            Assert.NotEqual(Guid.Empty, result);

            _mockUsuarioRepository.Verify(r => r.GetById(usuarioId), Times.Once);
            _mockDoacaoRepository.Verify(r => r.AdicionarAsync(It.IsAny<DoacaoRegistro>()), Times.Once);
            _mockDoacaoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}