using DOS.Core.DomainObjects;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.Eventos;
using DOS.Doacao.Application.Services.Agenda;
using DOS.Doacao.Application.Services.Usuario;
using DOS.Doacao.Domain;
using DOS.Doacao.Domain.Enums;


namespace DOS.Doacao.Application.CommandsHandlers
{
    public class AgendarDoacaoCommandHandler : ICommandHandler<AgendarDoacaoCommand, Guid>
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUsuarioService _usuarioService;
        private readonly IAgendaService _agendaService;
        public AgendarDoacaoCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioService usuarioService,
            IDomainEventDispatcher domainEventDispatcher,
            IAgendaService agendaService)
        {
            _doacaoRepository = doacaoRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _usuarioService = usuarioService;
            _agendaService = agendaService;
        }
        public async Task<Guid> HandleAsync(AgendarDoacaoCommand command)
        {

            var possuiDoacaoAtiva = await _doacaoRepository.ObterPorUsuarioAsync(command.UserId);
            var possuiDoacaoEmAndamento = possuiDoacaoAtiva.Any(d =>
            d.Status == StatusDoacao.Agendada ||
            d.Status == StatusDoacao.EmAndamento);
            if (possuiDoacaoEmAndamento)
            {
                throw new AppException("Você já possui algum processo de doação em andamento",409);
            }

            var usuario = await _usuarioService.ObterUsuarioPorId(command.UserId);
            if (usuario == null)
            {
                throw new AppException("Usuário não encontrado",404);
            }
            var dataHoraAgenda = await _agendaService.ObterAgendaPorId(command.AgendaId);
            var doacao = new DoacaoRegistro(
            command.AgendaId,
            command.UserId,
            usuario.TipoSanguineo,
            dataHoraAgenda.DataHora

        );
            await _doacaoRepository.AdicionarAsync(doacao);
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new AppException("Erro ao agendar a doação",500);
            }
            await _domainEventDispatcher.DispatchEventsAsync(
                  new List<IDomainEvent>
                  {
                    new DoacaoAgendadaEvent(
                        usuario.Nome,
                        usuario.Email,
                        doacao.Id,
                        doacao.DataHoraAgendada,
                        command.AgendaId
                    )
                  }
        );

            return doacao.Id;

        }
    }
}
