using DOS.Auth.Domain.Models;
namespace DOS.Auth.Application.Services.Interfaces
{
    public interface ITokenJWT
    {
        Task<string> GerarToken(User user);
        Task<string> GerarTokenRecuperacaoSenhaAsync(User user);
        bool ValidarTokenRecuperacaoSenha(string token, string email);


    }
}