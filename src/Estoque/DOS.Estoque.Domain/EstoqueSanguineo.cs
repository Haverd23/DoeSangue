using DOS.Core.DomainObjects;
using DOS.Estoque.Domain.Enums;

namespace DOS.Estoque.Domain
{
    public class EstoqueSanguineo : Entity, IAggregateRoot
    {
        public TipoSanguineo Tipo { get; private set; }
        public int Unidades { get; private set; }

        private int _contadorDoacoes;

        protected EstoqueSanguineo() { }

        public EstoqueSanguineo(TipoSanguineo tipo)
        {
            Tipo = tipo;
            Unidades = 0;
            _contadorDoacoes = 0;
        }
        public void RegistrarDoacao()
        {
            _contadorDoacoes++;

            if (_contadorDoacoes >= 5)
            {
                Unidades++;
                _contadorDoacoes = 0;
            }
        }

        public void RetirarUnidade()
        {
            if (Unidades <= 0)
                throw new Exception("Não há unidades disponíveis para este tipo sanguíneo.");

            Unidades--;
        }
    }
}
    

