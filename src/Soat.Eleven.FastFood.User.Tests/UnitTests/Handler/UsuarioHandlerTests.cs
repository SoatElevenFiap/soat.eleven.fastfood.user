using AutoFixture;
using Moq;
using NUnit.Framework;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Handlers;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Handler;

[TestFixture]
public class UsuarioHandlerTests
{
    private Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private Mock<IAuthenticationService> _authenticationServiceMock;
    private Mock<IPasswordService> _passwordServiceMock;
    private UsuarioHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _fixture = new Fixture();

        _handler = new UsuarioHandler(
            _usuarioRepositoryMock.Object,
            _authenticationServiceMock.Object,
            _passwordServiceMock.Object);
    }

    [Test]
    public async Task GetUsuario_WhenCalled_ShouldReturnUsuario()
    {
        // Arrange
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Cliente,
            Status = StatusUsuario.Ativo
        };

        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns(usuario);

        // Act
        var result = await _handler.GetUsuario();

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioOutputDto>());
    }

    [Test]
    public async Task AtualizarSenha_WhenUserNotAuthenticated_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<AtualizarSenhaInputDto>();
        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns((Usuario)null);

        // Act
        var result = await _handler.AtualizarSenha(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Usuário não autenticado."));
    }

    [Test]
    public async Task AtualizarSenha_WhenCurrentPasswordIncorrect_ShouldReturnError()
    {
        // Arrange
        var input = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual",
            NewPassword = "novaSenha"
        };

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Senha = "hashSenhaAtual"
        };

        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns(usuario);
        _passwordServiceMock.Setup(x => x.Hash(input.CurrentPassword)).Returns("hashSenhaIncorreta");

        // Act
        var result = await _handler.AtualizarSenha(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Senha atual incorreta."));
    }

    [Test]
    public async Task AtualizarSenha_WhenValidInput_ShouldReturnSuccess()
    {
        // Arrange
        var input = new AtualizarSenhaInputDto
        {
            CurrentPassword = "senhaAtual",
            NewPassword = "novaSenha"
        };

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Senha = "hashSenhaAtual"
        };

        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns(usuario);
        _passwordServiceMock.Setup(x => x.Hash(input.CurrentPassword)).Returns("hashSenhaAtual");
        _passwordServiceMock.Setup(x => x.Hash(input.NewPassword)).Returns("hashNovaSenha");

        // Act
        var result = await _handler.AtualizarSenha(input);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.True);

        Assert.That(usuario.Senha, Is.EqualTo("hashNovaSenha"));
        _usuarioRepositoryMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
    }

    [Test]
    public async Task AtualizarSenha_WhenInvalidInput_ShouldReturnValidationError()
    {
        // Arrange
        var input = new AtualizarSenhaInputDto
        {
            CurrentPassword = "", // Invalid - empty password
            NewPassword = "123"   // Invalid - too short
        };

        // Act
        var result = await _handler.AtualizarSenha(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.TypeOf<List<string>>());
    }
}