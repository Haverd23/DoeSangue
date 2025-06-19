using DOS.Agenda.Domain;

namespace Agenda.Tests.Domain
{
    public class HorarioTests
    {
        [Fact(DisplayName = "Criar horário de doação com dados válidos deve instanciar com sucesso")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1); 
            var vagasTotais = 10;

            // Act
            var horario = new Horario(dataHora, vagasTotais);

            // Assert
            Assert.NotNull(horario);
            Assert.Equal(dataHora, horario.DataHora);
            Assert.Equal(vagasTotais, horario.VagasTotais);
            Assert.Equal(0, horario.VagasOcupadas);
            Assert.True(horario.TemVagasDisponiveis());
        }
        [Fact(DisplayName = "Criar horário de doação com vagas totais inválidas deve lançar exceção")]
        public void Construtor_QuandoVagasTotaisInvalido_DeveLancarExcecao()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = -1;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Horario(dataHora, vagasTotais));
            Assert.Equal("O número total de vagas deve ser maior que zero.", ex.Message);
        }
        [Fact(DisplayName = "Criar horário de doação com data no passado deve lançar exceção")]
        public void Construtor_QuandoDataHoraNoPassado_DeveLancarExcecao()
        {
            // Arrange
            var dataHora = DateTime.Now.AddMinutes(-10); 
            var vagasTotais = 10;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Horario(dataHora, vagasTotais));
            Assert.Equal("A data e hora do horário não podem ser no passado.", ex.Message);
        }
        [Fact(DisplayName = "Reservar vaga deve incrementar o número de vagas ocupadas")]
        public void ReservarVaga_DeveIncrementarVagasOcupadas()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var horario = new Horario(dataHora, vagasTotais);

            // Act
            horario.ReservarVaga();

            // Assert
            Assert.Equal(1, horario.VagasOcupadas);
            Assert.True(horario.TemVagasDisponiveis());
        }
        [Fact(DisplayName = "Reservar vaga quando não há vagas disponíveis deve lançar exceção")]
        public void ReservarVaga_QuandoSemVagasDisponiveis_DeveLancarExcecao()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 1;
            var horario = new Horario(dataHora, vagasTotais);
            horario.ReservarVaga(); 

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => horario.ReservarVaga());
            Assert.Equal("Não há vagas disponíveis para este horário.", ex.Message);
        }
        [Fact(DisplayName = "Liberar vaga deve decrementar o número de vagas ocupadas")]
        public void LiberarVaga_DeveDecrementarVagasOcupadas()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var horario = new Horario(dataHora, vagasTotais);
            horario.ReservarVaga();

            // Act
            horario.LiberarVaga();

            // Assert
            Assert.Equal(0, horario.VagasOcupadas);
            Assert.True(horario.TemVagasDisponiveis());
        }
        [Fact(DisplayName = "Liberar vaga quando não há vagas ocupadas deve lançar exceção")]
        public void LiberarVaga_QuandoSemVagasOcupadas_DeveLancarExcecao()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var horario = new Horario(dataHora, vagasTotais);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => horario.LiberarVaga());
            Assert.Equal("Não há vagas ocupadas para liberar.", ex.Message);
        }
        [Fact(DisplayName = "Verificar se há vagas disponíveis deve retornar true quando houver vagas")]
        public void TemVagasDisponiveis_QuandoHouverVagas_DeveRetornarTrue()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 10;
            var horario = new Horario(dataHora, vagasTotais);

            // Act
            var resultado = horario.TemVagasDisponiveis();

            // Assert
            Assert.True(resultado);
        }
        [Fact(DisplayName = "Verificar se há vagas disponíveis deve retornar false quando não houver vagas")]
        public void TemVagasDisponiveis_QuandoNaoHouverVagas_DeveRetornarFalse()
        {
            // Arrange
            DateTime dataHora = DateTime.UtcNow.AddDays(1);
            var vagasTotais = 1;
            var horario = new Horario(dataHora, vagasTotais);
            horario.ReservarVaga();

            // Act
            var resultado = horario.TemVagasDisponiveis();

            // Assert
            Assert.False(resultado);
        }
        [Fact(DisplayName = "Alterar quantidade de vagas disponíveis deve atualizar a quantidade de vagas")]
        public void AlterarQuantidadeVagas_QuandoQuantidadeValida_DeveAtualizarNumeroDeVaga()
        {
            // Arrange
            DateTime datahora = DateTime.UtcNow.AddDays(1);
            var vasgasTotais = 1;
            var horario = new Horario(datahora, vasgasTotais);
            var vagasAtualizadas = 10;

            // Act
            horario.AlterarQuantidadeVagas(vagasAtualizadas);

            // Assert
            Assert.Equal(vagasAtualizadas, horario.VagasTotais);
        }
        [Fact(DisplayName = "Alterar vagas disponíveis com valor inválido deve lançar exceção")]
        public void AlterarQuantidadeVagas_QuandoQuandidadeInvalida_DeveLancarExcecao()
        {
            // Arrange
            DateTime datahora = DateTime.UtcNow.AddDays(1);
            var vasgasTotais = 1;
            var horario = new Horario(datahora, vasgasTotais);
            var vagasAtualizadas = 0;

            // Act & Assert
            var ex = Assert.Throws<Exception>( () => horario.AlterarQuantidadeVagas(vagasAtualizadas));
            Assert.Equal("Quantidade de vagas deve ser maior que 0", ex.Message);
        }
        [Fact(DisplayName = "Alterar horário válido com data e hora válidos")]
        public void AlterarDataHora_QuandoHorarioValido_DeveAtualizarHorario()
        {
            // Arrange
            DateTime datahora = DateTime.UtcNow.AddDays(1);
            var vasgasTotais = 1;
            var horario = new Horario(datahora, vasgasTotais);
            var horarioAtualizado = DateTime.UtcNow.AddDays(2);

            // Act
            horario.AlterarDataHora(horarioAtualizado);

            // Assert
            Assert.Equal(horarioAtualizado, horario.DataHora);

        }
        [Fact(DisplayName = "Alterar horário com data e hora inválidos")]
        public void AlterarDataHora_QuandoHorarioInvalido_DeveLancarExececao()
        {
            // Arrange
            DateTime datahora = DateTime.UtcNow.AddDays(1);
            var vasgasTotais = 1;
            var horario = new Horario(datahora, vasgasTotais);
            var horarioAtualizado = DateTime.UtcNow.AddDays(-2);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => horario.AlterarDataHora(horarioAtualizado));
            Assert.Equal("A data e hora do horário não podem ser no passado.", ex.Message);

        }
    }
}
