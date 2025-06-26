
using DOS.Core.DomainObjects;
using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoCanceladaCommandHandler : ICommandHandler<DoacaoCanceladaCommand,bool>
    {
        private readonly IDoacaoRepository _repository;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public DoacaoCanceladaCommandHandler(IDoacaoRepository repository,
            IDomainEventDispatcher domainEventDispatcher)
        {
            _repository = repository;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<bool> HandleAsync(DoacaoCanceladaCommand command)
        {
            var doacao =  await _repository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
            {
                throw new ApplicationException("Doação não encontrada");
            }
            doacao.Cancelar();
            var sucesso = await _repository.UnitOfWork.Commit();
            if(!sucesso)
            {
                throw new ApplicationException("Não foi possível atualizar status da doação para cancelado");
            }
            await _domainEventDispatcher.DispatchEventsAsync(doacao.DomainEvents);
            return true;

        }
    }
}
