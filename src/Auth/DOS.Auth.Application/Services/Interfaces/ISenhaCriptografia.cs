namespace DOS.Auth.Application.Services.Interfaces
{
    public interface ISenhaCriptografia
    {
        string SenhaHash(string password);
        bool VerificarSenha(string password, string storedHash);
    }
}