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
public class AdministradorHandlerTests
{
    private Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private Mock<IAuthenticationService> _authenticationServiceMock;
    private AdministradorHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _authenticationServiceMock = new Mock<IAuthenticationService>();
        _fixture = new Fixture();

        _handler = new AdministradorHandler(
            _usuarioRepositoryMock.Object,
            _authenticationServiceMock.Object);
    }

    [Test]
    public async Task CriarAdministrador_WhenEmailExists_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<CriarAdmInputDto>();
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(true);

        // Act
        var result = await _handler.CriarAdministrador(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Usuário já existe"));
    }

    [Test]
    public async Task CriarAdministrador_WhenValidInput_ShouldReturnSuccess()
    {
        // Arrange
        var input = new CriarAdmInputDto
        {
            Nome = "Admin",
            Email = "admin@email.com",
            Senha = "123456",
            Telefone = "11999999999"
        };

        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);

        // Act
        var result = await _handler.CriarAdministrador(input);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioAdmOutputDto>());
        _usuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Test]
    public async Task AtualizarAdminstrador_WhenAdministradorNotFound_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<AtualizaAdmInputDto>();
        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns(null as Usuario);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Administrador não encontrado"));
    }

    [Test]
    public async Task AtualizarAdminstrador_WhenEmailExists_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<AtualizaAdmInputDto>();
        var administrador = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "Admin",
            Email = "admin@email.com",
            Perfil = PerfilUsuario.Administrador
        };

        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns(administrador);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(true);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        // Assert
        Assert.That(result.Success, Is.False);
    }

    [Test]
    public async Task AtualizarAdminstrador_WhenValidInput_ShouldReturnSuccess()
    {
        // Arrange
        var input = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin Atualizado",
            Email = "admin.novo@email.com",
            Telefone = "11888888888"
        };

        var administrador = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "Admin",
            Email = "admin@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Administrador
        };

        _authenticationServiceMock.Setup(x => x.GetUsuario()).Returns(administrador);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioAdmOutputDto>());

        Assert.That(administrador.Nome, Is.EqualTo(input.Nome));
        Assert.That(administrador.Email, Is.EqualTo(input.Email));
        Assert.That(administrador.Telefone, Is.EqualTo(input.Telefone));

        _usuarioRepositoryMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
    }
}