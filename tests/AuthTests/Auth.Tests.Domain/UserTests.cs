using DOS.Auth.Domain.Models;

namespace Auth.Tests.Domain
{
    [Trait("Auth", "Auth Domain")]

    public class UserTests
    {

        [Fact(DisplayName = "Criar user com dados válidos deve instanciar com sucesso")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            var email = new Email("teste123@gmail.com");
            var senha = "Teste123@";
            var role = "Admin";

            // Act
            var user = new User(email, senha, role);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal(senha, user.Senha);
            Assert.Equal(role, user.Role);

        }
        [Fact(DisplayName = "Criar user com email inválido deve lançar exceção")]
        public void Construtor_QuandoEmailInvalido_DeveLancarExcecao()
        {
            // Arrange
            var senha = "12345";
            var role = "Admin";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new User(new Email("1"), senha, role));
        }
        [Fact(DisplayName = "Criar user com senha vazia deve lançar exceção")]
        public void Construtor_QuandoSenhaVazia_DeveLancarExcecao()
        {
            // Arrange
            var email = new Email("teste123@gmail.com");
            var senha = "";
            var role = "Admin";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(email, senha, role));

            // Assert
            Assert.Equal("Senha inválida", ex.Message);
        }
        [Fact(DisplayName = "Criar user com senha menor que 6 caracteres deve lançar exceção")]
        public void Constrator_QuandoSenhaMenorQue6Caracteres_DeveLancarExcecao()
        {
            // Arrange
            var email = new Email("teste123@gmail.com");
            var senha = "12345";
            var role = "Admin";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(email, senha, role));

            // Assert
            Assert.Equal("Senha deve ter pelo menos 6 caracteres", ex.Message);

        }
        [Fact(DisplayName = "Criar user com senha sem digito deve lançar exceção")]
        public void Construtor_QuandoSenhaNaoPossuiDigito_DeveLancerExcecao()
        {
            // Arrange
            var email = new Email("teste123@gmail.com");
            var senha = "SenhaSemDigito";
            var role = "Admin";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(email, senha, role));

            // Assert
            Assert.Equal("Senha deve conter pelo menos um número", ex.Message);
        }
        [Fact(DisplayName = "Criar user com senha sem letra deve lançar exceção")]
        public void Construtor_QuandoSenhaSemDigito_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "123456";
            var role = "Admin";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(new Email(email), senha, role));

            // Assert
            Assert.Equal("Senha deve conter pelo menos uma letra maiúscula", ex.Message);

        }
        [Fact(DisplayName = "Criar user com senha sem letra maiúscula deve lançar exceção")]
        public void Construtor_QuandoSenhaNaoPossuiMaiuscula_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "teste123";
            var role = "Admin";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(new Email(email), senha, role));

            // Assert
            Assert.Equal("Senha deve conter pelo menos uma letra maiúscula", ex.Message);
        }
        [Fact(DisplayName = "Criar user com senha sem letra minúscula deve lançar exceção")]
        public void Construtor_QuandoSenhaNaoPossuiMinuscula_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "TESTE123";
            var role = "Admin";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => new User(new Email(email), senha, role));

            // Assert
            Assert.Equal("Senha deve conter pelo menos uma letra minúscula", ex.Message);
        }
        [Fact(DisplayName = "Alterar senha com dados válidos deve alterar com sucesso")]
        public void AlterarSenha_QuandoSenhaValida_DeveTrocarDeSenha()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "Teste123@";
            var role = "Admin";

            var user = new User(new Email(email), senha, role);
            var novaSenha = "NovaSenha123@";

            // Act
            user.AlterarSenha(novaSenha);

            // Assert
            Assert.Equal(novaSenha, user.Senha);
        }
        [Fact(DisplayName = "Alterar senha com senha vazia deve lançar exceção")]
        public void AlterarSenha_QuandoSenhaVazia_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "Teste123@";
            var role = "Admin";

            var user = new User(new Email(email), senha, role);
            var novaSenha = "";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => user.AlterarSenha(novaSenha));

            // Assert
            Assert.Equal("Senha inválida", ex.Message);
        }
        [Fact(DisplayName = "Alterar senha com senha menor que 6 caracteres deve lançar exceção")]
        public void AlterarSenha_QuandoSenhaMenorQue6Caracteres_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "Teste123@";
            var role = "Admin";

            var user = new User(new Email(email), senha, role);
            var novaSenha = "12Dx@";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => user.AlterarSenha(novaSenha));

            // Assert
            Assert.Equal("Senha deve ter pelo menos 6 caracteres", ex.Message);

        }
        [Fact(DisplayName = "Alterar email com dados válidos deve alterar com sucesso")]
        public void AlterarEmail_QuandoEmailValido_DeveTrocarDeEmail()
        {
            // Arrange
            var email = "teste123@hotmail.com";
            var senha = "Teste123@";
            var role = "Admin";

            var user = new User(new Email(email), senha, role);
            var novoEmail = new Email("teste123@yahoo.com");

            // Act
            user.AlterarEmail(novoEmail);

            // Assert
            Assert.Equal(novoEmail, user.Email);
        }
        [Fact(DisplayName = "Alterar email com email inválido deve lançar exceção")]
        public void AlterarEmail_QuandoEmailInvalido_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste123@gmail.com";
            var senha = "Teste123@";
            var role = "Admin";

            var user = new User(new Email(email), senha, role);
           

            // Act
            var ex = Assert.Throws<ArgumentException>(() => user.AlterarEmail(new Email("1")));

            // Assert
            Assert.Equal("Email Inválido", ex.Message);
        }

    }
}
