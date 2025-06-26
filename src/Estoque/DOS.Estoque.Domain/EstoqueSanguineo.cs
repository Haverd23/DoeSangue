using DOS.Core.DomainObjects;
using DOS.Core.Enums;
using DOS.Core.Exceptions;

namespace DOS.Estoque.Domain
{
    public class EstoqueSanguineo : Entity, IAggregateRoot
    {
        public TipoSanguineo Tipo { get; private set; }
        public int Unidades { get; private set; }

        public int ContadorDoacoes { get; private set; }

        protected EstoqueSanguineo() { }

        public EstoqueSanguineo(TipoSanguineo tipo)
        {
            Tipo = tipo;
            Unidades = 0;
            ContadorDoacoes = 0;
        }
        public bool RegistrarDoacao()
        {
            ContadorDoacoes++;

            if (ContadorDoacoes >= 5)
            {
                Unidades++;
                ContadorDoacoes = 0;
                return true; 
            }

            return false; 
        }


        public void RetirarUnidade(int quantidade)
        {
            if (Unidades <= 0)
                throw new DomainException("Não há unidades disponíveis para este tipo sanguíneo.");
            if(quantidade > Unidades)
                throw new DomainException("Não há unidades suficiente para este tipo sanguíneo.");

            Unidades-=quantidade;
        }
    }
}
    

