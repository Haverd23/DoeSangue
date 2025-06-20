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
            var horario = DateTime.UtcNow.AddDays(1);
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
        [Fact(DisplayName = "Deve atualizar horário da agenda e retornar true")]
        public async Task HandleAsync_DeveAtualizarHorario_DeveRetornarTrue()
        {
            // Arrange
            var horario = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var command = new AgendaCriadaCommand(horario, vagasTotais);
            var handler = new AgendaCriadaCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);
            var result = await handler.HandleAsync(command);
            var horarioAtualizado = DateTime.UtcNow.AddDays(2);

            var horarioCommand = new AtualizarDataHoraCommand(result, horarioAtualizado);
            var horarioHandler = new AtualizarDataHoraCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.ObterPorIdAsync(result))
                .ReturnsAsync(new Horario(horario, vagasTotais));

            // Act
            var horarioResult = await horarioHandler.HandleAsync(horarioCommand);

            // Assert
            Assert.True(horarioResult);
            Assert.Equal(horarioAtualizado, horarioCommand.DataHora);
            _mockHorarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Exactly(2));
            _mockHorarioRepository.Verify(x => x.ObterPorIdAsync(result), Times.Once);
        }
        [Fact(DisplayName = "Deve atualizar quantidade de vagas e retornar true")]
        public async Task HandleAsync_DeveAtualizarQuantidadeDeVagas_DeveRetornarTrue()
        {
            // Arrange
            var horario = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var command = new AgendaCriadaCommand(horario, vagasTotais);
            var handler = new AgendaCriadaCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);
            var result = await handler.HandleAsync(command);
            var vagasAtualizadas = 30;

            var vagasCommand = new AtualizarQuantidadeVagasCommand(result, vagasAtualizadas);
            var vagasHandler = new AtualizarQuantidadeVagasCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.ObterPorIdAsync(result))
                .ReturnsAsync(new Horario(horario, vagasAtualizadas));

            // Act
            var vagasResult = await vagasHandler.HandleAsync(vagasCommand);

            // Assert
            Assert.True(vagasResult);
            Assert.Equal(vagasAtualizadas, vagasCommand.Quantidade);
            _mockHorarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Exactly(2));
            _mockHorarioRepository.Verify(x => x.ObterPorIdAsync(result), Times.Once);
        }
        [Fact(DisplayName = "Deve deletar horario e retornar true")]
        public async Task HandleAsync_DeveDeletarHorario_DeveRetornarTrue()
        {
            // Arrange
            var horario = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var command = new AgendaCriadaCommand(horario, vagasTotais);
            var handler = new AgendaCriadaCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.UnitOfWork.Commit())
                .ReturnsAsync(true);
            var result = await handler.HandleAsync(command);

            var deletarCommand = new DeletarAgendaCommand(result);
            var deletarHandler = new DeletarAgendaCommandHandler(_mockHorarioRepository.Object);
            _mockHorarioRepository.Setup(x => x.ObterPorIdAsync(result))
                 .ReturnsAsync(new Horario(horario, vagasTotais));

            // Act
            var deletarResult = await deletarHandler.HandleAsync(deletarCommand);

            // Assert
            Assert.True(deletarResult);
            _mockHorarioRepository.Verify(x => x.UnitOfWork.Commit(), Times.Exactly(2));
            _mockHorarioRepository.Verify(x => x.ObterPorIdAsync(result), Times.Once);
        }
    }
}
