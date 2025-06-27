using DOS.Notificacao.Application.Events.Doacao;
using DOS.Notificacao.Application.Kafka;
using DOS.Notificacao.Domain;
namespace DOS.Notificacao.Application.EventsHandlers.Doacao
{
    public class DoacaoFalhaEventHandler : IKafkaEventHandler<DoacaoFalhaEvent>
    {
        private readonly IEmailService _emailService;

        public DoacaoFalhaEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(DoacaoFalhaEvent evento)
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
                    color: #f39c12;
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
                    background-color: #f39c12;
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;
                    font-weight: bold;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                <h2>Solicitação de Comparecimento</h2>
                <p>Olá,</p>
                <p>Informamos que sua doação recente necessita de uma verificação adicional.</p>
                <p>Por gentileza, pedimos que compareça pessoalmente à nossa unidade para tratarmos de informações importantes relacionadas à sua doação.</p>
                <p>Estamos à disposição para qualquer esclarecimento.</p>
            </div>
        </body>
        </html>";

            await _emailService.EnviarEmailAsync(
                evento.Email,
                "Solicitação de Comparecimento – Doação de Sangue",
                corpoEmail
            );
        }
    }
}