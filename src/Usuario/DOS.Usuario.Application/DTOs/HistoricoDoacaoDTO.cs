namespace DOS.Usuario.Application.DTOs
{
    public class HistoricoDoacaoDTO
    {
        public DateTime DataHora { get; set; }
        public string Status {  get; set; }

        public HistoricoDoacaoDTO(DateTime dataHora, string status)
        {
            DataHora = dataHora;
            Status = status;
        }
    }
}
