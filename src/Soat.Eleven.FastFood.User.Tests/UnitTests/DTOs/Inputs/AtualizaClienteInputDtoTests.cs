using Soat.Eleven.FastFood.User.Application.DTOs.Inputs;
using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Inputs;

[TestFixture]
public class AtualizaClienteInputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new AtualizaClienteInputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "João Silva Atualizado";
        var email = "joao.novo@email.com";
        var telefone = "11888888888";
        var clienteId = Guid.NewGuid();
        var cpf = "98765432101";
        var dataDeNascimento = DateTime.Now.AddYears(-25);

        // Act
        var dto = new AtualizaClienteInputDto
        {
            Id = id,
            Nome = nome,
            Email = email,
            Telefone = telefone,
            ClienteId = clienteId,
            Cpf = cpf,
            DataDeNascimento = dataDeNascimento
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.Nome, Is.EqualTo(nome));
            Assert.That(dto.Email, Is.EqualTo(email));
            Assert.That(dto.Telefone, Is.EqualTo(telefone));
            Assert.That(dto.ClienteId, Is.EqualTo(clienteId));
            Assert.That(dto.Cpf, Is.EqualTo(cpf));
            Assert.That(dto.DataDeNascimento, Is.EqualTo(dataDeNascimento));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConverted_ShouldReturnCorrectCliente()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var dto = new AtualizaClienteInputDto
        {
            Id = id,
            Nome = "João Silva Atualizado",
            Email = "joao.novo@email.com",
            Telefone = "11888888888",
            ClienteId = clienteId,
            Cpf = "98765432101",
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Id, Is.EqualTo(clienteId));
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Id, Is.EqualTo(id));
            Assert.That(cliente.Usuario.Nome, Is.EqualTo(dto.Nome));
            Assert.That(cliente.Usuario.Email, Is.EqualTo(dto.Email));
            Assert.That(cliente.Usuario.Telefone, Is.EqualTo(dto.Telefone));
            Assert.That(cliente.Cpf, Is.EqualTo(dto.Cpf));
            Assert.That(cliente.DataDeNascimento, Is.EqualTo(dto.DataDeNascimento));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithEmptyGuid_ShouldReturnClienteWithEmptyGuid()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.Empty,
            Nome = "João Silva",
            Email = "joao@email.com",
            Telefone = "11999999999",
            ClienteId = Guid.Empty,
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Id, Is.EqualTo(Guid.Empty));
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Id, Is.EqualTo(Guid.Empty));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithEmptyStringValues_ShouldReturnClienteWithEmptyProperties()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = string.Empty,
            Email = string.Empty,
            Telefone = string.Empty,
            ClienteId = Guid.NewGuid(),
            Cpf = string.Empty,
            DataDeNascimento = default(DateTime)
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Nome, Is.EqualTo(string.Empty));
            Assert.That(cliente.Usuario.Email, Is.EqualTo(string.Empty));
            Assert.That(cliente.Usuario.Telefone, Is.EqualTo(string.Empty));
            Assert.That(cliente.Cpf, Is.EqualTo(string.Empty));
            Assert.That(cliente.DataDeNascimento, Is.EqualTo(default(DateTime)));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedWithEmptyStrings_ShouldReturnClienteWithEmptyProperties()
    {
        // Arrange
        var dto = new AtualizaClienteInputDto
        {
            Id = Guid.NewGuid(),
            Nome = "",
            Email = "",
            Telefone = "",
            ClienteId = Guid.NewGuid(),
            Cpf = "",
            DataDeNascimento = DateTime.MinValue
        };

        // Act
        Cliente cliente = dto;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(cliente, Is.Not.Null);
            Assert.That(cliente.Usuario, Is.Not.Null);
            Assert.That(cliente.Usuario.Nome, Is.EqualTo(""));
            Assert.That(cliente.Usuario.Email, Is.EqualTo(""));
            Assert.That(cliente.Usuario.Telefone, Is.EqualTo(""));
            Assert.That(cliente.Cpf, Is.EqualTo(""));
            Assert.That(cliente.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
        }
    }
}