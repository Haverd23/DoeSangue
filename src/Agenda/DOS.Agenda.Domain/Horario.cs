using DOS.Core.DomainObjects;
using DOS.Core.Exceptions;

namespace DOS.Agenda.Domain
{
    public class Horario : Entity, IAggregateRoot
    {
        public DateTime DataHora { get; private set; }
        public int VagasTotais { get; private set; }
        public int VagasOcupadas { get; private set; }

        protected Horario() { }

        public Horario(DateTime dataHora, int vagasTotais)
        {
            DataHora = dataHora;
            VagasTotais = vagasTotais;
            VagasOcupadas = 0;

            Validar();

        }
        public void AlterarQuantidadeVagas(int vagas)
        {
            if (vagas <= 0)
                throw new DomainException("Quantidade de vagas deve ser maior que 0");
            VagasTotais = vagas;
        }
        public void AlterarDataHora(DateTime dataHora)
        {
            if (dataHora < DateTime.UtcNow)
                throw new DomainException("A data e hora do horário não podem ser no passado.");
            DataHora = dataHora;
        }
        public bool TemVagasDisponiveis()
        {
            return VagasOcupadas < VagasTotais;
        }
        public void ReservarVaga()
        {
            if (!TemVagasDisponiveis())
                throw new DomainException("Não há vagas disponíveis para este horário.");
            VagasOcupadas++;
        }
        public void LiberarVaga()
        {
            if (VagasOcupadas <= 0)
                throw new DomainException("Não há vagas ocupadas para liberar.");
            VagasOcupadas--;
        }
        private void Validar()
        {
            if (VagasTotais <= 0)
                throw new DomainException("O número total de vagas deve ser maior que zero.");
            if (DataHora < DateTime.UtcNow)
                throw new DomainException("A data e hora do horário não podem ser no passado.");
        }
    }
}
