using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Core.Mediator.Queries;
using DOS.Usuario.Application.DTOs;
using DOS.Usuario.Application.Queries;
using DOS.Usuario.Application.Services;
using DOS.Usuario.Domain;

namespace DOS.Usuario.Application.QueriesHandlers
{
    public class DoacaoHistoricoQueryHandler : IQueryHandler<DoacaoHistoricoQuery,
        IEnumerable<HistoricoDoacaoDTO>>
    {
        private readonly IUsuarioRepository _repository;
        private readonly IDoacaoService _dacaoService;

        public DoacaoHistoricoQueryHandler(IUsuarioRepository repository,
            IDoacaoService doacaoService)
        {
            _repository = repository;
            _dacaoService = doacaoService;
        }

        public async Task<IEnumerable<HistoricoDoacaoDTO>> HandleAsync(DoacaoHistoricoQuery query)
        {
            var usuario = await _repository.GetById(query.UsuarioID);
            if (usuario == null) throw new AppException("Usuário não encontrado",404);
            var doacao = await _dacaoService.ObterDoacaoPorId(usuario.Id);
            if (doacao == null) throw new AppException("Nenhuma doação encontrada", 404);
            return doacao;

        }
    }
}
