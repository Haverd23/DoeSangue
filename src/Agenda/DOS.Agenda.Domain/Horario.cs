using DOS.Core.DomainObjects;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;

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
                throw new AppException("Quantidade de vagas deve ser maior que 0",400);
            VagasTotais = vagas;
        }
        public void AlterarDataHora(DateTime dataHora)
        {
            if (dataHora < DateTime.UtcNow)
                throw new AppException("A data e hora do horário não podem ser no passado.",400);
            DataHora = dataHora;
        }
        public bool TemVagasDisponiveis()
        {
            return VagasOcupadas < VagasTotais;
        }
        public void ReservarVaga()
        {
            if (!TemVagasDisponiveis())
                throw new AppException("Não há vagas disponíveis para este horário.",409);
            VagasOcupadas++;
        }
        public void LiberarVaga()
        {
            if (VagasOcupadas <= 0)
                throw new AppException("Não há vagas ocupadas para liberar.", 400);
            VagasOcupadas--;
        }
        private void Validar()
        {
            if (VagasTotais <= 0)
                throw new AppException("O número total de vagas deve ser maior que zero.", 400);
            if (DataHora < DateTime.UtcNow)
                throw new AppException("A data e hora do horário não podem ser no passado.", 400);
        }
    }
}
