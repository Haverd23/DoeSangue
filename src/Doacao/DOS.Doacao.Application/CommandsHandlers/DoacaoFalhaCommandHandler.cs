using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoFalhaCommandHandler : ICommandHandler<DoacaoFalhaCommand,bool>
    {
        private readonly IDoacaoRepository _doacaoRepository;

        public DoacaoFalhaCommandHandler(IDoacaoRepository doacaoRepository)
        {
            _doacaoRepository = doacaoRepository;
        }

        public async Task<bool> HandleAsync(DoacaoFalhaCommand command)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
            {
                throw new Exception("Doação não encontrada");
            }
            doacao.MarcarComoFalha();
            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
            {
                throw new Exception("Erro em alterar status da doação");
            }
            return true;
        }
    }
}
