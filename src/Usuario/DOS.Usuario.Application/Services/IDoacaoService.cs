using DOS.Usuario.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS.Usuario.Application.Services
{
    public interface IDoacaoService
    {
        public Task<IEnumerable<HistoricoDoacaoDTO>> ObterDoacaoPorId(Guid Id);
    }
}
