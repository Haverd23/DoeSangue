using DOS.Auth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOS.Auth.Application.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Autenticar(Email email, string senha);
    }
}