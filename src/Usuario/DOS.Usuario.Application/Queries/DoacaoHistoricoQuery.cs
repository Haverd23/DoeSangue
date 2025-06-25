using DOS.Core.Mediator.Queries;
using DOS.Usuario.Application.DTOs;

namespace DOS.Usuario.Application.Queries
{
    public class DoacaoHistoricoQuery : IQuery<IEnumerable<HistoricoDoacaoDTO>>
    {
        public Guid UsuarioID { get; set; }

        public DoacaoHistoricoQuery(Guid usuarioID)
        {
            UsuarioID = usuarioID;
        }
    }
}
