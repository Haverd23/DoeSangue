using DOS.Notificacao.Application.Events.Doacao;
using DOS.Notificacao.Application.Kafka;
using DOS.Notificacao.Domain;

namespace DOS.Notificacao.Application.EventsHandlers.Doacao
{
    public class DoacaoAgendadaEventHandler : IKafkaEventHandler<DoacaoAgendadaEvent>
    {
        private readonly IEmailService _emailService;

        public DoacaoAgendadaEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task HandleAsync(DoacaoAgendadaEvent evento)
        {
            var dataHoraFormatada = evento.DataHoraAgendada.ToString("dd/MM/yyyy 'às' HH:mm");

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
                    .info {{
                        background-color: #f9f9f9;
                        padding: 10px;
                        border-radius: 6px;
                        margin-top: 10px;
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
                    <h2>Doação Agendada com Sucesso!</h2>
                    <p>Olá, {evento.Nome},</p>
                    <p>Você agendou uma doação de sangue para:</p>
                    <div class='info'>
                        <p><strong>Data e Hora:</strong> {dataHoraFormatada}</p>
             
                    </div>
                    <p>Obrigado por fazer a diferença! 💉❤️</p>
                    <a class='button' href='#'>Ver detalhes no sistema</a>
                </div>
            </body>
            </html>";

            await _emailService.EnviarEmailAsync(
                evento.Email,
                "Confirmação de Agendamento de Doação",
                corpoEmail
            );
        }
    }
}

