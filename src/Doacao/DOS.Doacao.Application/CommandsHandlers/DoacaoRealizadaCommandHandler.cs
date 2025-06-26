using DOS.Core.DomainObjects;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Application.Eventos;
using DOS.Doacao.Application.Services.Usuario;
using DOS.Doacao.Domain;
using DOS.Usuario.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoRealizadaCommandHandler : ICommandHandler<DoacaoRealizadaCommand, bool>  
    {
        private readonly IDoacaoRepository _doacaoRepository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IUsuarioService _usuarioService;

        public DoacaoRealizadaCommandHandler(IDoacaoRepository doacaoRepository,
            IUsuarioService usuarioService,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _doacaoRepository = doacaoRepository;
            _domainEventDispatcher = domainEventDispatcher;
            _usuarioService = usuarioService;
        }

        public async Task<bool> HandleAsync(DoacaoRealizadaCommand command)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
                throw new AppException("Doação não encontrada", 404);

            doacao.Iniciar();

            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
                throw new AppException("Erro ao iniciar a doação.",500);

            var usuario = await _usuarioService.ObterUsuarioPorId(doacao.UsuarioId);
            if (usuario == null)
                throw new AppException("Usuário da doação não encontrado",404);

            await _domainEventDispatcher.DispatchEventsAsync(
                  new List<IDomainEvent>
                  {
                    new DoacaoRealizadaEvent(
                        doacao.Id,
                        usuario.Email,
                        usuario.Nome
                    )
                  }
        );

            return true;
        }
    }
}