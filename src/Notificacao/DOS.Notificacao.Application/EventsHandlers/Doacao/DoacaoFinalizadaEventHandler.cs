
using DOS.Notificacao.Application.Events.Doacao;
using DOS.Notificacao.Application.Kafka;
using DOS.Notificacao.Domain;

namespace DOS.Notificacao.Application.EventsHandlers.Doacao
{
    public class DoacaoFinalizadaEventHandler : IKafkaEventHandler<DoacaoFinalizadaEvent>
    {
        private readonly IEmailService _emailService;

        public DoacaoFinalizadaEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(DoacaoFinalizadaEvent evento)
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
                    color: #27ae60;
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
                    background-color: #27ae60;
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;
                    font-weight: bold;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Doação Finalizada com Sucesso!</h2>
                <p>Olá, {evento.Nome},</p>
                <p>Temos o prazer de informar que sua doação passou por todas as etapas de análise e foi <strong>finalizada com sucesso</strong>.</p>
                <p>Seu gesto generoso está agora contribuindo diretamente para salvar vidas. ❤️</p>
                <p>Em nome de todos que serão beneficiados, agradecemos imensamente sua solidariedade!</p>
            </div>
        </body>
        </html>";

            await _emailService.EnviarEmailAsync(
                evento.Email,
                "Sua Doação foi Finalizada com Sucesso – Obrigado por Ajudar!",
                corpoEmail
            );
        }
    }
}