using DOS.Agenda.Application.Commands;
using DOS.Agenda.Application.CommandsHandlers;
using DOS.Agenda.Domain;
using Moq;

namespace Agenda.Tests.Application
{
    public class CommandsTests
    {
        private readonly Mock<IHorarioRepository> _mockHorarioRepository;
        public CommandsTests()
        {
            _mockHorarioRepository = new Mock<IHorarioRepository>();
        }

        [Fact(DisplayName = "Deve criar agenda e retornar Id")]
        public async Task HandleAsync_DeveCriarAgendaERetornarId()
        {
            // Arrange
            var horario = new DateTime(2025, 10, 10, 10, 0, 0);
            var vagasTotais = 10;

            var command = new AgendaCriadaCommand(horario, vagasTotais);
            var handler = new AgendaCriadaCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);

            // Act
            var result = await handler.HandleAsync(command);


            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockHorarioRepository.Verify(x => x.Adicionar(It.IsAny<Horario>()), Times.Once);
            _mockHorarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Once);

        }
    }
}
