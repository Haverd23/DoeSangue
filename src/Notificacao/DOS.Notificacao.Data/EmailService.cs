using DOS.Notificacao.Domain;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;

namespace DOS.Notificacao.Data
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;
        private readonly bool _smtpUseSsl;

        public EmailService(string smtpHost, int smtpPort,
            string smtpUser, string smtpPass, bool smtpUseSsl)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
            _smtpUseSsl = smtpUseSsl;
        }

        public async Task EnviarEmailAsync(string para, string assunto, string corpo)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Equipe Notificações", _smtpUser));
            message.To.Add(MailboxAddress.Parse(para));
            message.Subject = assunto;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = corpo
            };

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            var socketOption = _smtpUseSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;
            await client.ConnectAsync(_smtpHost, _smtpPort, socketOption);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}

