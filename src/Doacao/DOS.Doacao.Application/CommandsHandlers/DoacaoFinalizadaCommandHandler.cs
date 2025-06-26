using DOS.Core.DomainObjects;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.Eventos;
using DOS.Doacao.Application.Services.Usuario;
using DOS.Doacao.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoFinalizadaCommandHandler : ICommandHandler<DoacaoFinalizadaCommand,bool>
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUsuarioService _usuarioService;

        public DoacaoFinalizadaCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioService usuarioService,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _doacaoRepository = doacaoRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _usuarioService = usuarioService;
        }

        public async Task<bool> HandleAsync(DoacaoFinalizadaCommand command)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
            {
                throw new AppException("Doação não encontrada",404);
            }
            doacao.Finalizar();
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new AppException("Erro ao finalizar doação",500);
            }
            var usuario = await _usuarioService.ObterUsuarioPorId(doacao.UsuarioId);
            if (usuario == null)
                throw new AppException("Usuário da doação não encontrado", 404);
            var tipoSanguineo = usuario.TipoSanguineo.ToString();
            await _domainEventDispatcher.DispatchEventsAsync(
                new List<IDomainEvent>
                {
                  new DoacaoFinalizadaEvent(
                      doacao.Id,
                      usuario.Nome,
                      usuario.Email,
                      tipoSanguineo
                      )
                });
            return true;

            
        }
    }
}
