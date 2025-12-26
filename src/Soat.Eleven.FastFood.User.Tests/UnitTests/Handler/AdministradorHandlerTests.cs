using AutoFixture;
using Moq;
using NUnit.Framework;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Handlers;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;
using Soat.Eleven.FastFood.User.Domain.ErrorValidators;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Handler;

[TestFixture]
public class AdministradorHandlerTests
{
    private Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private Mock<IJwtTokenService> _jwtTokenServiceMock;
    private AdministradorHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _fixture = new Fixture();

        _handler = new AdministradorHandler(
            _usuarioRepositoryMock.Object,
            _jwtTokenServiceMock.Object);
    }

    [Test]
    public async Task CriarAdministrador_WhenEmailExists_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<CriarAdmInputDto>();
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(true);

        // Act
        var result = await _handler.CriarAdministrador(input);

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Data, Is.EqualTo(ErrorMessages.USER_FOUND));
        }
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

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.TypeOf<UsuarioAdmOutputDto>());
        }
        _usuarioRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Usuario>()), Times.Once);
    }

    [Test]
    public async Task CriarAdministrador_WhenValidationFails_ShouldReturnError()
    {
        // Arrange
        var input = new CriarAdmInputDto
        {
            Nome = string.Empty, // Invalid - empty name
            Email = "invalid-email", // Invalid - invalid email format
            Senha = "123", // Invalid - too short
            Telefone = string.Empty // Invalid - empty phone
        };

        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);

        // Act
        var result = await _handler.CriarAdministrador(input);

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Data, Is.TypeOf<List<string>>());
        }
        var errors = (List<string>)result.Data;
        Assert.That(errors, Is.Not.Empty);
    }

    [Test]
    public async Task AtualizarAdminstrador_WhenAdministradorNotFound_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<AtualizaAdmInputDto>();
        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(Guid.NewGuid);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Usuario);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Data, Is.EqualTo(ErrorMessages.ADMIN_NOT_FOUND));
        }
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

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(Guid.NewGuid);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(true);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(administrador);

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

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(administrador.Id);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(administrador);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.True);
            Assert.That(result.Data, Is.TypeOf<UsuarioAdmOutputDto>());
            Assert.That(administrador.Nome, Is.EqualTo(input.Nome));
            Assert.That(administrador.Email, Is.EqualTo(input.Email));
            Assert.That(administrador.Telefone, Is.EqualTo(input.Telefone));
        }

        _usuarioRepositoryMock.Verify(x => x.Update(It.IsAny<Usuario>()), Times.Once);
    }

    [Test]
    public async Task AtualizarAdministrador_WhenAdministradorNotFound_ShouldReturnError()
    {
        // Arrange
        var input = new AtualizaAdmInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "Admin",
            Email = "admin@email.com",
            Telefone = "11999999999"
        };

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(Guid.NewGuid());
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(null as Usuario);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Data, Is.EqualTo(ErrorMessages.ADMIN_NOT_FOUND));
        }
    }

    [Test]
    public async Task AtualizarAdministrador_WhenValidationFails_ShouldReturnError()
    {
        // Arrange
        var administradorId = Guid.NewGuid();
        var input = new AtualizaAdmInputDto
        {
            Id = Guid.Empty, // Invalid - empty ID
            Nome = string.Empty, // Invalid - empty name
            Email = "invalid-email", // Invalid - invalid email format
            Telefone = new string('9', 20) // Invalid - exceeds max length
        };

        var administrador = new Usuario
        {
            Id = administradorId,
            Nome = "Admin Original",
            Email = "original@email.com",
            Telefone = "11999999999",
            Perfil = PerfilUsuario.Administrador
        };

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(administradorId);
        _usuarioRepositoryMock.Setup(x => x.GetByIdAsync(administradorId)).ReturnsAsync(administrador);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);

        // Act
        var result = await _handler.AtualizarAdminstrador(input);

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(result.Success, Is.False);
            Assert.That(result.Data, Is.TypeOf<List<string>>());
        }
        var errors = (List<string>)result.Data;
        Assert.That(errors, Is.Not.Empty);
    }
}