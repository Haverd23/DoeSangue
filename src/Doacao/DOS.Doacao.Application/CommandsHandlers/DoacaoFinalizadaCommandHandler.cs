using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.Eventos;
using DOS.Doacao.Domain;
using DOS.Usuario.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoFinalizadaCommandHandler : ICommandHandler<DoacaoFinalizadaCommand,bool>
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DoacaoFinalizadaCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioRepository usuarioRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _doacaoRepository = doacaoRepository;
            _usuarioRepository = usuarioRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<bool> HandleAsync(DoacaoFinalizadaCommand command)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
            {
                throw new Exception("Doação não encontrada");
            }
            doacao.Finalizar();
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new Exception("Erro ao finalizar doação");
            }
            var usuario = await _usuarioRepository.GetById(doacao.UsuarioId);
            if (usuario == null)
                throw new Exception("Usuário da doação não encontrado.");

            await _domainEventDispatcher.DispatchEventsAsync(
                new List<IDomainEvent>
                {
                  new DoacaoFinalizadaEvent(
                      doacao.Id,
                      usuario.Nome,
                      usuario.Email
                      )
                });
            return true;

            
        }
    }
}
