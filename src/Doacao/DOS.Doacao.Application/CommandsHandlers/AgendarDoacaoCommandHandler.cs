using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.Eventos;
using DOS.Doacao.Domain;
using DOS.Doacao.Domain.Enums;
using DOS.Usuario.Domain;


namespace DOS.Doacao.Application.CommandsHandlers
{
    public class AgendarDoacaoCommandHandler : ICommandHandler<AgendarDoacaoCommand, Guid>
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        public AgendarDoacaoCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioRepository usuarioRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _doacaoRepository = doacaoRepository;
            _usuarioRepository = usuarioRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }
        public async Task<Guid> HandleAsync(AgendarDoacaoCommand command)
        {

            var possuiDoacaoAtiva = await _doacaoRepository.ObterPorUsuarioAsync(command.UserId);
            var possuiDoacaoEmAndamento = possuiDoacaoAtiva.Any(d =>
            d.Status == StatusDoacao.Agendada ||
            d.Status == StatusDoacao.EmAndamento);
            if (possuiDoacaoEmAndamento)
            {
                throw new Exception("Você já possui algum processo de doação em andamento.");
            }

            var usuario = await _usuarioRepository.GetById(command.UserId);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
           
            string? tipoSanguineo = usuario?.TipoSanguineo?.ToString();

            var doacao = new DoacaoRegistro(
            command.AgendaId,
            command.UserId,
            tipoSanguineo,
            command.DataHoraAgendada
        );
            await _doacaoRepository.AdicionarAsync(doacao);
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new Exception("Erro ao agendar a doação");
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
