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
    private Mock<IJwtTokenService> _jwtTokenServiceMock;
    private Mock<IPasswordService> _passwordServiceMock;
    private UsuarioHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _fixture = new Fixture();

        _handler = new UsuarioHandler(
            _usuarioRepositoryMock.Object,
            _jwtTokenServiceMock.Object,
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

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuario.Id);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);

        // Act
        var result = await _handler.GetUsuario();

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioOutputDto>());
    }

    [Test]
    public async Task GetUsuario_WhenUsuarioIsCliente_ShouldReturnUsuarioClienteOutputDto()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Cliente,
            Status = StatusUsuario.Ativo,
            Cliente = new Cliente
            {
                Id = clienteId,
                Cpf = "12345678901",
                DataDeNascimento = DateTime.Now.AddYears(-30)
            }
        };

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuario.Id);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);

        // Act
        var result = await _handler.GetUsuario();

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioClienteOutputDto>());

        var output = (UsuarioClienteOutputDto)result.Data;
        Assert.That(output.ClientId, Is.EqualTo(clienteId));
        Assert.That(output.Cpf, Is.EqualTo("12345678901"));
        Assert.That(output.Nome, Is.EqualTo("João Silva"));
    }

    [Test]
    public async Task GetUsuario_WhenUsuarioNotFound_ShouldReturnError()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuarioId);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(usuarioId)).ReturnsAsync(null as Usuario);

        // Act
        var result = await _handler.GetUsuario();

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Usuário não autenticado."));
    }

    [Test]
    public async Task AtualizarSenha_WhenUserNotAuthenticated_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<AtualizarSenhaInputDto>();
        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(Guid.NewGuid);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Usuario);

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

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuario.Id);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);
        _passwordServiceMock.Setup(x => x.TransformToHash(input.CurrentPassword)).Returns("hashSenhaIncorreta");

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

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuario.Id);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(usuario.Id)).ReturnsAsync(usuario);
        _passwordServiceMock.Setup(x => x.TransformToHash(input.CurrentPassword)).Returns("hashSenhaAtual");
        _passwordServiceMock.Setup(x => x.TransformToHash(input.NewPassword)).Returns("hashNovaSenha");

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