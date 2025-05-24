using DOS.Usuario.Domain;
using DOS.Usuario.Domain.Enums;

namespace UsuarioTests.Domain
{
    public class UsuarioTests
    {
        [Fact(DisplayName = "Criar usuário com dados válidos")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            var nome = "João";
            var email = "email@gmail.com";
            var cpf = "12345678909";
            var telefone = "11987654321";
            var tipoSanguineo = TipoSanguineo.ABNegativo;

            // Act
            var usuario = new Usuario(nome, email, cpf, telefone, tipoSanguineo);

            // Assert
            Assert.NotNull(usuario);
            Assert.Equal(nome, usuario.Nome);
            Assert.Equal(email, usuario.Email);
            Assert.Equal(cpf, usuario.CPF.Numero);
            Assert.Equal(telefone, usuario.Telefone.Numero);
            Assert.Equal(tipoSanguineo, usuario.TipoSanguineo);
        }
        [Fact(DisplayName = "Criar usuário com CPF inválido deve lançar exceção")]
        public void Construtor_QuandoCPFInvalido_DeveLancerExcecao()
        {
            // Arrange
            var nome = "João";
            var email = "email.com@gmail.com";
            var cpf = "1234567890"; 
            var telefone = "11987654321";
            var tipoSanguineo = TipoSanguineo.ABNegativo;

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Usuario(nome, email, cpf, telefone, tipoSanguineo));
        }
        [Fact(DisplayName = "Criar usuário com nome inválido deve lançar exceção")]
        public void Construtor_QuandoNomeInvalido_DeveLancerExcecao()
        {
            // Arrange
            var nome = "J"; 
            var email = "1234567890";
            var cpf = "12345678909";
            var telefone = "11987654321";
            var tipoSanguineo = TipoSanguineo.ABNegativo;

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Usuario(nome, email, cpf, telefone, tipoSanguineo));
        }
        [Fact(DisplayName = "Criar usuário com telefone inválido deve lançar exceção")]
        public void Construtor_QuandoTelefoneInvalido_DeveLancarExececao()
        {
            // Arrange
            var nome = "João";
            var email = "1234567890";
            var cpf = "12345678909";
            var telefone = "119876543";
            var tipoSanguineo = TipoSanguineo.ABNegativo;

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Usuario(nome, email, cpf, telefone, tipoSanguineo));
        }
        [Fact(DisplayName = "Alterar telefone com telefone inválido deve lançar exceção")]
        public void AlterarTelefone_QuandoTelefoneInvalido_DeveLancarExcecao()
        {
            // Arrange
            var nome = "João";
            var email = "1234567890";
            var cpf = "12345678909";
            var telefone = "11987654321";
            var tipoSanguineo = TipoSanguineo.ABNegativo;
            var usuario = new Usuario(nome, email, cpf, telefone, tipoSanguineo);
            // Act & Assert
            var ex = Assert.Throws<Exception>(() => usuario.AlterTelefone("119876543"));
        }
        [Fact(DisplayName = "Alterar telefone com telefone válido deve alterar com sucesso")]
        public void AlterarTelefone_QuandoTelefoneValido_DeveAlterarComSucesso()
        {
            // Arrange
            var nome = "João";
            var email = "1234567890";
            var cpf = "12345678909";
            var telefone = "11987654321";
            var tipoSanguineo = TipoSanguineo.ABNegativo;
            var usuario = new Usuario(nome, email, cpf, telefone, tipoSanguineo);
            // Act
            usuario.AlterTelefone("11987654322");
            // Assert
            Assert.Equal("11987654322", usuario.Telefone.Numero);
        }

    }
}
