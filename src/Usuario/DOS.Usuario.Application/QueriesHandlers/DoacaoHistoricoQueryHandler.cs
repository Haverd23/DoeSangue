using DOS.Core.Mediator.Queries;
using DOS.Doacao.Domain;
using DOS.Usuario.Application.DTOs;
using DOS.Usuario.Application.Queries;
using DOS.Usuario.Domain;

namespace DOS.Usuario.Application.QueriesHandlers
{
    public class DoacaoHistoricoQueryHandler : IQueryHandler<DoacaoHistoricoQuery,
        IEnumerable<HistoricoDoacaoDTO>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IDoacaoRepository _doacaoRepository;

        public DoacaoHistoricoQueryHandler(IUsuarioRepository repository,
            IDoacaoRepository doacaoRepository)
        {
            _repository = repository;
            _doacaoRepository = doacaoRepository;
        }

        public async Task<IEnumerable<HistoricoDoacaoDTO>> HandleAsync(DoacaoHistoricoQuery query)
        {
            var usuario = await _repository.GetById(query.UsuarioID);
            if (usuario == null) throw new ApplicationException("Usuário não encontrado");
            var doacao = await _doacaoRepository.ObterPorUsuarioAsync(usuario.Id);
            return doacao.Select(d => new HistoricoDoacaoDTO
            {
                DataHora = d.DataHoraAgendada,
                Status = d.Status.ToString(),
            });
        }
    }
}
