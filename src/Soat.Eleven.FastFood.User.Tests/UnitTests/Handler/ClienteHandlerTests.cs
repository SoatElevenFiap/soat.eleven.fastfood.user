using AutoFixture;
using Moq;
using NUnit.Framework;
using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Application.Handlers;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Repositories;
using Soat.Eleven.FastFood.User.Domain.Interfaces.Services;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Handler;

[TestFixture]
public class ClienteHandlerTests
{
    private Mock<IClienteRepository> _clienteRepositoryMock;
    private Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private Mock<IJwtTokenService> _jwtTokenServiceMock;
    private Mock<IPasswordService> _passwordServiceMock;
    private ClienteHandler _handler;
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _jwtTokenServiceMock = new Mock<IJwtTokenService>();
        _passwordServiceMock = new Mock<IPasswordService>();
        _fixture = new Fixture();

        _handler = new ClienteHandler(
            _clienteRepositoryMock.Object,
            _usuarioRepositoryMock.Object,
            _jwtTokenServiceMock.Object,
            _passwordServiceMock.Object);
    }

    [Test]
    public async Task InserirCliente_WhenEmailExists_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<CriarClienteInputDto>();
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(true);

        // Act
        var result = await _handler.InserirCliente(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Usuário já existe"));
    }

    [Test]
    public async Task InserirCliente_WhenCpfExists_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<CriarClienteInputDto>();
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);
        _clienteRepositoryMock.Setup(x => x.ExistByCpf(input.Cpf)).ReturnsAsync(true);

        // Act
        var result = await _handler.InserirCliente(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Usuário já existe"));
    }

    [Test]
    public async Task InserirCliente_WhenValidInput_ShouldReturnSuccess()
    {
        // Arrange
        var input = new CriarClienteInputDto
        {
            Nome = "João Silva",
            Email = "joao@email.com",
            Senha = "123456",
            Telefone = "11999999999",
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);
        _clienteRepositoryMock.Setup(x => x.ExistByCpf(input.Cpf)).ReturnsAsync(false);

        // Act
        var result = await _handler.InserirCliente(input);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioClienteOutputDto>());
        _clienteRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Cliente>()), Times.Once);
    }

    [Test]
    public async Task AtualizarCliente_WhenUserNotAuthenticated_ShouldReturnError()
    {
        // Arrange
        var input = _fixture.Create<AtualizaClienteInputDto>();
        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(Guid.Empty);

        // Act
        var result = await _handler.AtualizarCliente(input);

        // Assert
        Assert.That(result.Success, Is.False);
        Assert.That(result.Data, Is.EqualTo("Usuário não autenticado"));
    }

    [Test]
    public async Task AtualizarCliente_WhenEmailExists_ShouldReturnError()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var input = _fixture.Create<AtualizaClienteInputDto>();

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuarioId);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(true);

        // Act
        var result = await _handler.AtualizarCliente(input);

        // Assert
        Assert.That(result.Success, Is.False);
    }

    [Test]
    public async Task AtualizarCliente_WhenCpfExists_ShouldReturnError()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var input = _fixture.Create<AtualizaClienteInputDto>();

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuarioId);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);
        _clienteRepositoryMock.Setup(x => x.ExistByCpf(input.Cpf)).ReturnsAsync(true);

        // Act
        var result = await _handler.AtualizarCliente(input);

        // Assert
        Assert.That(result.Success, Is.False);
    }

    [Test]
    public async Task AtualizarCliente_WhenValidInput_ShouldReturnSuccess()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var input = new AtualizaClienteInputDto
        {
            Id = usuarioId,
            ClienteId = clienteId,
            Nome = "João Silva Atualizado",
            Email = "joao.novo@email.com",
            Telefone = "11888888888",
            Cpf = "98765432101",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        var cliente = new Cliente
        {
            Id = usuarioId,
            Usuario = new Usuario
            {
                Id = usuarioId,
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "11999999999"
            },
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        _jwtTokenServiceMock.Setup(x => x.GetUsuarioId()).Returns(usuarioId);
        _usuarioRepositoryMock.Setup(x => x.ExistEmail(input.Email)).ReturnsAsync(false);
        _clienteRepositoryMock.Setup(x => x.ExistByCpf(input.Cpf)).ReturnsAsync(false);
        _clienteRepositoryMock.Setup(x => x.GetByIdAsync(usuarioId)).ReturnsAsync(cliente);

        // Act
        var result = await _handler.AtualizarCliente(input);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioClienteOutputDto>());
        _clienteRepositoryMock.Verify(x => x.Update(It.IsAny<Cliente>()), Times.Once);
    }

    [Test]
    public async Task GetClienteByCPF_WhenClienteExists_ShouldReturnSuccess()
    {
        // Arrange
        var cpf = "12345678901";
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Cpf = cpf,
            DataDeNascimento = DateTime.Now.AddYears(-30),
            Usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "11999999999"
            }
        };

        _clienteRepositoryMock.Setup(x => x.GetByCPF(cpf)).ReturnsAsync(cliente);

        // Act
        var result = await _handler.GetClienteByCPF(cpf);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.TypeOf<UsuarioClienteOutputDto>());

        var output = (UsuarioClienteOutputDto)result.Data;
        Assert.That(output.Cpf, Is.EqualTo(cpf));
        Assert.That(output.Nome, Is.EqualTo(cliente.Usuario.Nome));
        Assert.That(output.Email, Is.EqualTo(cliente.Usuario.Email));
    }

    [Test]
    public async Task GetClienteByCPF_WhenClienteNotExists_ShouldReturnNull()
    {
        // Arrange
        var cpf = "12345678901";
        _clienteRepositoryMock.Setup(x => x.GetByCPF(cpf)).ReturnsAsync(null as Cliente);

        // Act
        var result = await _handler.GetClienteByCPF(cpf);

        // Assert
        Assert.That(result.Success, Is.True);
        Assert.That(result.Data, Is.Null);
    }
}