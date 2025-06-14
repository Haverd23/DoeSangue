namespace DOS.Notificacao.Domain
{
    public interface IEmailService
    {
      Task EnviarEmailAsync(string para, string assunto, string corpo);
    }
}
