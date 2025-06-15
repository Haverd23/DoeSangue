using DOS.Notificacao.Application.Events.Doacao;
using DOS.Notificacao.Application.Kafka;
using DOS.Notificacao.Domain;

namespace DOS.Notificacao.Application.EventsHandlers.Doacao
{
    public class DoacaoRealizadaEventHandler : IKafkaEventHandler<DoacaoRealizadaEvent>
    {
        private readonly IEmailService _emailService;

        public DoacaoRealizadaEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(DoacaoRealizadaEvent evento)
        {
            var corpoEmail = $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    background-color: #ffffff;
                    padding: 20px;
                    margin: 30px auto;
                    max-width: 600px;
                    border-radius: 8px;
                    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
                }}
                h2 {{
                    color: #e74c3c;
                }}
                p {{
                    font-size: 16px;
                    color: #333333;
                    line-height: 1.5;
                }}
                .button {{
                    display: inline-block;
                    margin-top: 20px;
                    padding: 10px 20px;
                    background-color: #e74c3c;
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;
                    font-weight: bold;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Obrigado por sua doação, {evento.Nome}!</h2>
                <p>Parabéns! Sua doação foi concluída com sucesso no <strong>Sistema de Doação de Sangue</strong>.</p>
                <p>Você fez a diferença na vida de quem precisa. ❤️</p>
                <p>Nosso time agradece imensamente sua solidariedade e empenho em ajudar o próximo.</p>
            </div>
        </body>
        </html>";

            await _emailService.EnviarEmailAsync(
                evento.Email,
                "Doação Realizada com Sucesso – Obrigado por salvar vidas!",
                corpoEmail
            );
        }
    }
}