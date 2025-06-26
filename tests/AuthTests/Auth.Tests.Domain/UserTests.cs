using DOS.Auth.Domain.Models;

namespace Auth.Tests.Domain
{
    using DOS.Auth.Domain.Models;
    using DOS.Core.Exceptions;
    using DOS.Core.Exceptions.DOS.Core.Exceptions;
    using System;
    using Xunit;

    namespace Auth.Tests.Domain
    {
        [Trait("Auth", "Auth Domain")]
        public class UserTests
        {
            [Fact(DisplayName = "Criar user com dados válidos deve instanciar com sucesso")]
            public void Construtor_QuandoDadosValidos_DeveInstanciar()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";

                // Act
                var user = new User(email, senha);

                // Assert
                Assert.NotNull(user);
                Assert.Equal(email, user.Email.Entrada);
                Assert.Equal(senha, user.Senha);
                Assert.Equal("User;", user.Role);
            }

            [Fact(DisplayName = "Criar user com email inválido deve lançar exceção")]
            public void Construtor_QuandoEmailInvalido_DeveLancarExcecao()
            {
                // Arrange
                var email = "invalido";
                var senha = "Teste123@";

                // Act & Assert
                var ex = Assert.Throws<AppException>(() => new User(email, senha));
                Assert.Equal("Email Inválido", ex.Message);
            }

            [Fact(DisplayName = "Criar user com senha vazia deve lançar exceção")]
            public void Construtor_QuandoSenhaVazia_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "";

                // Act
                var ex = Assert.Throws<AppException>(() => new User(email, senha));

                // Assert
                Assert.Equal("Senha inválida", ex.Message);
            }

            [Fact(DisplayName = "Criar user com senha menor que 6 caracteres deve lançar exceção")]
            public void Construtor_QuandoSenhaMenorQue6Caracteres_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "T12@";

                // Act
                var ex = Assert.Throws<AppException>(() => new User(email, senha));

                // Assert
                Assert.Equal("Senha deve ter pelo menos 6 caracteres", ex.Message);
            }

            [Fact(DisplayName = "Criar user com senha sem digito deve lançar exceção")]
            public void Construtor_QuandoSenhaNaoPossuiDigito_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "SenhaSemDigito";

                // Act
                var ex = Assert.Throws<AppException>(() => new User(email, senha));

                // Assert
                Assert.Equal("Senha deve conter pelo menos um número", ex.Message);
            }

            [Fact(DisplayName = "Criar user com senha sem letra maiúscula deve lançar exceção")]
            public void Construtor_QuandoSenhaSemMaiuscula_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "senha123@";

                // Act
                var ex = Assert.Throws<AppException>(() => new User(email, senha));

                // Assert
                Assert.Equal("Senha deve conter pelo menos uma letra maiúscula", ex.Message);
            }

            [Fact(DisplayName = "Criar user com senha sem letra minúscula deve lançar exceção")]
            public void Construtor_QuandoSenhaSemMinuscula_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "SENHA123@";

                // Act
                var ex = Assert.Throws<AppException>(() => new User(email, senha));

                // Assert
                Assert.Equal("Senha deve conter pelo menos uma letra minúscula", ex.Message);
            }

            [Fact(DisplayName = "Alterar senha com dados válidos deve alterar com sucesso")]
            public void AlterarSenha_QuandoSenhaValida_DeveAlterar()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";
                var user = new User(email, senha);
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
                var user = new User(email, senha);

                // Act
                var ex = Assert.Throws<AppException>(() => user.AlterarSenha(""));

                // Assert
                Assert.Equal("Senha inválida", ex.Message);
            }

            [Fact(DisplayName = "Alterar senha para menor que 6 caracteres deve lançar exceção")]
            public void AlterarSenha_QuandoSenhaMenorQue6_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";
                var user = new User(email, senha);

                // Act
                var ex = Assert.Throws<AppException>(() => user.AlterarSenha("T1@"));

                // Assert
                Assert.Equal("Senha deve ter pelo menos 6 caracteres", ex.Message);
            }

            [Fact(DisplayName = "Alterar email com dados válidos deve alterar com sucesso")]
            public void AlterarEmail_QuandoEmailValido_DeveAlterar()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";
                var user = new User(email, senha);
                var novoEmail = "teste123@yahoo.com";

                // Act
                user.AlterarEmail(novoEmail);

                // Assert
                Assert.Equal(novoEmail, user.Email.Entrada);
            }

            [Fact(DisplayName = "Alterar email com email inválido deve lançar exceção")]
            public void AlterarEmail_QuandoEmailInvalido_DeveLancarExcecao()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";
                var user = new User(email, senha);

                // Act
                var ex = Assert.Throws<AppException>(() => user.AlterarEmail("emailinvalido"));

                // Assert
                Assert.Equal("Email Inválido", ex.Message);
            }
            [Fact(DisplayName = "Alterar role com dados válidos")]
            public void AlterarRole_QuandoRoleValida_DeveAlterarComSucesso()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";
                var user = new User(email, senha);
                var roleNova = "Administrador";

                // Act
                user.AlterarRole(roleNova);

                // Assert
                Assert.Equal(roleNova, user.Role);
            }
            [Fact(DisplayName = "Alterar role dados inválido deve lançar exception")]
            public void AlterarRole_QuandoRoleInvalida_DeveLancarException()
            {
                // Arrange
                var email = "teste123@gmail.com";
                var senha = "Teste123@";
                var user = new User(email, senha);
                var roleNova = "Teste";

                // Act & Assert
                var ex = Assert.Throws<AppException>(() => user.AlterarRole(roleNova));
                Assert.Equal("Role inválida", ex.Message);
                
            }

        }
    }
}

