using DOS.Core.Enums;
using DOS.Core.Exceptions;
using DOS.Core.Exceptions.DOS.Core.Exceptions;
using DOS.Estoque.Domain;
using System.ComponentModel;

namespace EstoqueTests.Domain
{
    public class EstoqueTests
    {
        [Fact]
        [DisplayName("Deve iniciar com unidades zeradas")]
        public void Construtor_DeveIniciarComUnidadesZeradas()
        {
            // Arrange
            var estoque = new EstoqueSanguineo(TipoSanguineo.APositivo);

            // Act
            var unidades = estoque.Unidades;

            // Assert
            Assert.Equal(0, unidades);
        }
        [Fact]
        [DisplayName("Não deve adicionar unidade com menos de 5 doações")]
        public void RegistrarDoacao_ComMenosDe5_Doacoes_NaoIncrementaUnidade()
        {
            // Arrange
            var estoque = new EstoqueSanguineo(TipoSanguineo.APositivo);

            // Act
            for (int i = 0; i < 4; i++)
            {
                estoque.RegistrarDoacao();
            }

            // Assert
            Assert.Equal(0, estoque.Unidades);
        }
        [Fact]
        [DisplayName("Deve adicionar unidade com 5 doações")]
        public void RegistrarDoacao_Com5_Doacoes_IncrementaUnidade()
        {
            // Arrange
            var estoque = new EstoqueSanguineo(TipoSanguineo.APositivo);
            // Act
            for (int i = 0; i < 5; i++)
            {
                estoque.RegistrarDoacao();
            }
            // Assert
            Assert.Equal(1, estoque.Unidades);
        }
        [Fact]
        [DisplayName("Deve retirar unidade quando houver unidades disponíveis")]
        public void RetirarUnidade_ComUnidadesDisponiveis_DeveRetirarUnidade()
        {
            // Arrange
            var estoque = new EstoqueSanguineo(TipoSanguineo.APositivo);
            var quantidade = 1;
            for (int i = 0; i < 5; i++)
            {
                estoque.RegistrarDoacao();
            }
            // Act
            estoque.RetirarUnidade(quantidade);

            // Assert
            Assert.Equal(0, estoque.Unidades);
        }
        [Fact]
        [DisplayName("Não deve retirar unidade quando não houver unidades disponíveis")]
        public void RetirarUnidade_SemUnidadesDisponiveis_DeveLancarExcecao()
        {
            // Arrange
            var estoque = new EstoqueSanguineo(TipoSanguineo.APositivo);
            var quantidade = 1;

            // Act & Assert
            var exception = Assert.Throws<AppException>(() => estoque.RetirarUnidade(quantidade));
            Assert.Equal("Não há unidades disponíveis para este tipo sanguíneo", exception.Message);
        }
        [Fact]
        [DisplayName("Deve reiniciar contador de doações após adicionar unidade")]
        public void RegistrarDoacao_AposAdicionarUnidade_DeveReiniciarContador()
        {
            // Arrange
            var estoque = new EstoqueSanguineo(TipoSanguineo.APositivo);
            var quantidade = 1;
            for (int i = 0; i < 5; i++)
            {
                estoque.RegistrarDoacao();
            }
            // Act

            estoque.RetirarUnidade(quantidade);
            estoque.RegistrarDoacao();
            // Assert
            Assert.Equal(0, estoque.Unidades);
        }

    }
}
