using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Doacao.Domain;
using DOS.Usuario.Application.DTOs;

namespace DOS.Usuario.Application.Services
{
    public class DoacaoService : IDoacaoService
    {
        private readonly IDoacaoRepository _repository;

        public DoacaoService(IDoacaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HistoricoDoacaoDTO>> ObterDoacaoPorId(Guid Id)
        {
            var doacoes = await _repository.ObterPorUsuarioAsync(Id);
            return doacoes.Select(d => new HistoricoDoacaoDTO(
            d.DataHoraAgendada,
            d.Status.ToString()
            ));

        }
    }
}

