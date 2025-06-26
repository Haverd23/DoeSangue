using DOS.Core.DomainObjects;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.Eventos;
using DOS.Doacao.Domain;
using DOS.Usuario.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoFalhaCommandHandler : ICommandHandler<DoacaoFalhaCommand,bool>
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DoacaoFalhaCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioRepository usuarioRepository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _doacaoRepository = doacaoRepository;
            _usuarioRepository = usuarioRepository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<bool> HandleAsync(DoacaoFalhaCommand command)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
            {
                throw new AppException("Doação não encontrada",404);
            }
            doacao.MarcarComoFalha();
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new AppException("Erro em alterar status da doação", 500);
            }
            var usuario = await _usuarioRepository.GetById(doacao.UsuarioId);
            if (usuario == null)
            {
                throw new AppException("Usuario não encontrado", 404);
            }
            await _domainEventDispatcher.DispatchEventsAsync(
                new List<IDomainEvent>
                {
                    new DoacaoFalhaEvent(
                        doacao.Id,
                        usuario.Nome,
                        usuario.Email
                        )
                });
            return true;
        }
    }
}
