using DOS.Core.Enums;
using DOS.Estoque.Application.Commands;
using DOS.Estoque.Application.CommandsHandlers;
using DOS.Estoque.Domain;
using Moq;

namespace EstoqueTests.Application
{
    public class CommandTest
    {

    [Fact(DisplayName = "Deve adicionar novo estoque quando tipo sanguíneo não existir")]
    public async Task Deve_Adicionar_Estoque_Se_Tipo_Nao_Existir()
    {
        // Arrange
        var tipoSanguineo = TipoSanguineo.OPositivo;

        var repositoryMock = new Mock<IEstoqueRepository>();
        repositoryMock
            .Setup(r => r.ObterPorTipoAsync(tipoSanguineo))
            .ReturnsAsync((EstoqueSanguineo?)null);

        repositoryMock
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(true);

        var handler = new RegistrarDoacaoEstoqueCommandHandler(repositoryMock.Object);
        var command = new RegistrarDoacaoEstoqueCommand(tipoSanguineo);

        // Act
        var resultado = await handler.HandleAsync(command);

        // Assert
        Assert.True(resultado);
        repositoryMock.Verify(r => r.Adicionar(It.IsAny<EstoqueSanguineo>()), Times.Once);
        repositoryMock.Verify(r => r.Atualizar(It.IsAny<EstoqueSanguineo>()), Times.Never);
    }

    [Fact(DisplayName = "Deve atualizar estoque quando tipo sanguíneo já existir")]
    public async Task Deve_Atualizar_Estoque_Se_Tipo_Existir()
    {
        // Arrange
        var tipoSanguineo = TipoSanguineo.ONegativo;
        var estoqueExistente = new EstoqueSanguineo(tipoSanguineo);

        var repositoryMock = new Mock<IEstoqueRepository>();
        repositoryMock
            .Setup(r => r.ObterPorTipoAsync(tipoSanguineo))
            .ReturnsAsync(estoqueExistente);

        repositoryMock
            .Setup(r => r.UnitOfWork.Commit())
            .ReturnsAsync(true);

        var handler = new RegistrarDoacaoEstoqueCommandHandler(repositoryMock.Object);
        var command = new RegistrarDoacaoEstoqueCommand(tipoSanguineo);

        // Act
        var resultado = await handler.HandleAsync(command);

        // Assert
        Assert.True(resultado);
        repositoryMock.Verify(r => r.Atualizar(It.IsAny<EstoqueSanguineo>()), Times.Once);
        repositoryMock.Verify(r => r.Adicionar(It.IsAny<EstoqueSanguineo>()), Times.Never);
    }
}
}
