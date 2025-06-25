namespace DOS.Estoque.Application.DTOs
{
    public class EstoqueDTO
    {
        public Guid EstoqueId { get; set; }
        public string TipoSanguineo { get; set; }
        public int Unidades { get; set; }
        public int Contador {  get; set; }
    }
}
