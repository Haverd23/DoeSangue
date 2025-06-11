using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoFinalizadaCommandHandler : ICommandHandler<DoacaoFinalizadaCommand,bool>
    {
        private readonly IDoacaoRepository _doacaoRepository;

        public DoacaoFinalizadaCommandHandler(IDoacaoRepository repository)
        {
            _doacaoRepository = repository;
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
            return true;

            
        }
    }
}
