using DOS.Core.Mediator.Commands;
using DOS.Doacao.Application.Commands;
using DOS.Doacao.Domain;

namespace DOS.Doacao.Application.CommandsHandlers
{
    public class DoacaoRealizadaCommandHandler : ICommandHandler<DoacaoRealizadaCommand, bool>  
    {
        private readonly IDoacaoRepository _doacaoRepository;


        public DoacaoRealizadaCommandHandler(IDoacaoRepository doacaoRepository)
        {
            _doacaoRepository = doacaoRepository;
        
        }
        public async Task<bool> HandleAsync(DoacaoRealizadaCommand command)
        {
            var doacao = await _doacaoRepository.ObterPorIdAsync(command.DoacaoId);
            if (doacao == null)
                throw new Exception("Doação não encontrada.");

            doacao.Iniciar();

            var sucesso = await _doacaoRepository.UnitOfWork.Commit();
            if (!sucesso)
                throw new Exception("Erro ao iniciar a doação.");

            return true;
        }
    }
}