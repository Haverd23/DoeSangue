using DOS.Core.DomainObjects;
using DOS.Core.Enums;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.CommandsHandlers;
using DOS.Doacao.Application.DTOs;
using DOS.Doacao.Application.Services.Agenda;
using DOS.Doacao.Application.Services.Usuario;
using DOS.Doacao.Domain;
using Moq;


namespace DoacaoTests.Application
{
    public class CommandsTests
    {
        private readonly Mock<IDoacaoRepository> _mockDoacaoRepository;
        private readonly Mock<IAgendaService> _mockAgendaService;
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Mock<IDomainEventDispatcher> _mockDomainEventDispatcher;
        private readonly AgendarDoacaoCommandHandler _handler;

        public CommandsTests()
        {
            _mockDoacaoRepository = new Mock<IDoacaoRepository>();
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mockAgendaService = new Mock<IAgendaService>();
            _mockDomainEventDispatcher = new Mock<IDomainEventDispatcher>();

            _handler = new AgendarDoacaoCommandHandler(
                _mockDoacaoRepository.Object,
                _mockUsuarioService.Object,
                _mockDomainEventDispatcher.Object,
                _mockAgendaService.Object
            );
        }

        [Fact(DisplayName = "Deve criar doação e retornar Id")]
        public async Task HandleAsync_DeveCriarDoacaoERetornarId()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = TipoSanguineo.APositivo;

            var usuarioDto = new UsuarioDTO(
                "João Silva",
                "joao.silva@teste.com",
                tipoSanguineo.ToString()
            );

            var agendaDto = new AgendaDTO(agendaId, DateTime.Now.AddDays(1));


            _mockUsuarioService
                .Setup(r => r.ObterUsuarioPorId(usuarioId))
                .ReturnsAsync(usuarioDto);

            _mockAgendaService
                .Setup(r => r.ObterAgendaPorId(agendaId))
                .ReturnsAsync(agendaDto);

            _mockDoacaoRepository
                .Setup(r => r.AdicionarAsync(It.IsAny<DoacaoRegistro>()))
                .Returns(Task.CompletedTask);

            _mockDoacaoRepository
                .Setup(r => r.UnitOfWork.Commit())
                .ReturnsAsync(true);

            var command = new AgendarDoacaoCommand(agendaId)
            {
                UserId = usuarioId
            };

            // Act
            var result = await _handler.HandleAsync(command);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockUsuarioService.Verify(r => r.ObterUsuarioPorId(usuarioId), Times.Once);
            _mockAgendaService.Verify(r => r.ObterAgendaPorId(agendaId), Times.Once);
            _mockDoacaoRepository.Verify(r => r.AdicionarAsync(It.IsAny<DoacaoRegistro>()), Times.Once);
            _mockDoacaoRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

    }
}
