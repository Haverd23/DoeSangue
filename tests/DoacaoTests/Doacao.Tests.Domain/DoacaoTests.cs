using DOS.Doacao.Domain;
using DOS.Doacao.Domain.Enums;

namespace DoacaoTests.Application
{
    public class DoacaoTests
    {
        [Fact(DisplayName = "Criar doação com dados válidos")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = "AB-";
            var dataAgendada = DateTime.Now.AddDays(1);

            // Act
            var doacao = new DoacaoRegistro(agendaId, usuarioId, tipoSanguineo, dataAgendada);

            // Assert
            Assert.NotNull(doacao);
            Assert.Equal(agendaId, doacao.AgendaId);
            Assert.Equal(usuarioId, doacao.UsuarioId);
            Assert.Equal(tipoSanguineo, doacao.TipoSanguineo);
            Assert.Equal(dataAgendada.Date, doacao.DataHoraAgendada.Date);
        }
        [Fact(DisplayName = "Criar doação com data agendada no passado deve lançar exceção")]
        public void Construtor_QuandoDataAgendadaNoPassado_DeveLancarExcecao()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = "AB-";
            var dataAgendada = DateTime.Now.AddDays(-1);

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new DoacaoRegistro(agendaId, usuarioId, tipoSanguineo, dataAgendada));
            Assert.Equal("A data agendada deve ser futura.", ex.Message);
        }
        [Fact(DisplayName = "Iniciar doação agendada deve alterar status para EmAndamento")]
        public void Iniciar_QuandoDoacaoAgendada_DeveAlterarStatusParaEmAndamento()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = "AB-";
            var dataAgendada = DateTime.Now.AddDays(1);
            var doacao = new DoacaoRegistro(agendaId, usuarioId, tipoSanguineo, dataAgendada);

            // Act
            doacao.Iniciar();

            // Assert
            Assert.Equal(StatusDoacao.EmAndamento, doacao.Status);
        }
        [Fact(DisplayName = "Finalizar doação em andamento deve alterar status para Finalizada")]
        public void Finalizar_QuandoDoacaoEmAndamento_DeveAlterarStatusParaFinalizada()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = "AB-";
            var dataAgendada = DateTime.Now.AddDays(1);
            var doacao = new DoacaoRegistro(agendaId, usuarioId, tipoSanguineo, dataAgendada);
            doacao.Iniciar();

            // Act
            doacao.Finalizar();

            // Assert
            Assert.Equal(StatusDoacao.Finalizada, doacao.Status);
        }
        [Fact(DisplayName = "Cancelamento de doação em Agendada deve alterar status para Cancelada")]
        public void Cancelar_QuandoDoacaoAgendada_DeveAlterarStatusParaCancelada()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = "AB-";
            var dataAgendada = DateTime.Now.AddDays(1);
            var doacao = new DoacaoRegistro(agendaId, usuarioId, tipoSanguineo, dataAgendada);
          

            // Act
            doacao.Cancelar();

            // Assert
            Assert.Equal(StatusDoacao.Cancelada, doacao.Status);
        }
        [Fact(DisplayName = "Marcar doação em andamento como falha deve alterar status para Falha")]
        public void MarcarComoFalha_QuandoDoacaoEmAndamento_DeveAlterarStatusParaFalha()
        {
            // Arrange
            var agendaId = Guid.NewGuid();
            var usuarioId = Guid.NewGuid();
            var tipoSanguineo = "AB-";
            var dataAgendada = DateTime.Now.AddDays(1);
            var doacao = new DoacaoRegistro(agendaId, usuarioId, tipoSanguineo, dataAgendada);
            doacao.Iniciar();

            // Act
            doacao.MarcarComoFalha();

            // Assert
            Assert.Equal(StatusDoacao.Falha, doacao.Status);
        }
    }
}
