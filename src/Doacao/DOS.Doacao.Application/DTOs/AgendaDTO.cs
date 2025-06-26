namespace DOS.Doacao.Application.DTOs
{
    public class AgendaDTO
    {
        public Guid AgendaID { get; set; }
        public DateTime DataHora { get; set; }

        public AgendaDTO(Guid agendaID, DateTime dataHora)
        {
            AgendaID = agendaID;
            DataHora = dataHora;
        }
    }
}
