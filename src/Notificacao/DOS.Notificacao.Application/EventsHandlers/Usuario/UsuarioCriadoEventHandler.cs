using DOS.Notificacao.Application.Events.Usuario;
using DOS.Notificacao.Domain;

namespace DOS.Notificacao.Application.EventsHandlers.Usuario
{
    public class UsuarioCriadoEventHandler
    {
        private readonly IEmailService _emailService;

        public UsuarioCriadoEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task HandleAsync(UsuarioCriadoEvent evento)
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
                    <h2>Bem-vindo, {evento.Nome}!</h2>
                    <p>Seu cadastro no <strong>Sistema de Doação de Sangue</strong> foi realizado com sucesso.</p>
                    <p>Estamos muito felizes em ter você conosco nessa missão de <strong>salvar vidas</strong>.</p>

                    <div class='info'>
                        <p><strong>Nome:</strong> {evento.Nome}</p>
                        <p><strong>Email:</strong> {evento.Email}</p>
                    </div>

                    <p>Agora você pode acessar sua conta, agendar doações e acompanhar suas contribuições.</p>

                    <a class='button' href='#'>Acessar Minha Conta</a>
                </div>
            </body>
            </html>";

            await _emailService.EnviarEmailAsync(
                evento.Email,
                "Seja bem-vindo ao Sistema de Doação de Sangue!",
                corpoEmail
            );
        }
    }
}