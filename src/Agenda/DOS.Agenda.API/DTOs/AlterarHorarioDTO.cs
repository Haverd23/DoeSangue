namespace DOS.Agenda.API.DTOs
{
    public class AlterarHorarioDTO
    {
        public Guid AgendaId { get; set; }
        public DateTime Horario { get; set; }
    }
}
