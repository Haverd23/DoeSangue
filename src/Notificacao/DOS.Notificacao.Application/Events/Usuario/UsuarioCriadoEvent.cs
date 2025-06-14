namespace DOS.Notificacao.Application.Events.Usuario
{
    public class UsuarioCriadoEvent
    {
        public string Nome { get;  set; }
        public string Email { get;  set; }
        public DateTime OcurreuEm { get;  set; }
    }
}
