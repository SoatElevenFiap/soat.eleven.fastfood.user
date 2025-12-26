using Soat.Eleven.FastFood.User.Application.DTOs.Outputs;
using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.DTOs.Outputs;

[TestFixture]
public class UsuarioClienteOutputDtoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var dto = new UsuarioClienteOutputDto();

        // Assert
        Assert.That(dto, Is.Not.Null);
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clientId = Guid.NewGuid();
        var nome = "João Silva";
        var email = "joao@email.com";
        var telefone = "11999999999";
        var cpf = "12345678901";
        var dataDeNascimento = DateTime.Now.AddYears(-30);

        // Act
        var dto = new UsuarioClienteOutputDto
        {
            Id = id,
            ClientId = clientId,
            Nome = nome,
            Email = email,
            Telefone = telefone,
            Cpf = cpf,
            DataDeNascimento = dataDeNascimento
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto.Id, Is.EqualTo(id));
            Assert.That(dto.ClientId, Is.EqualTo(clientId));
            Assert.That(dto.Nome, Is.EqualTo(nome));
            Assert.That(dto.Email, Is.EqualTo(email));
            Assert.That(dto.Telefone, Is.EqualTo(telefone));
            Assert.That(dto.Cpf, Is.EqualTo(cpf));
            Assert.That(dto.DataDeNascimento, Is.EqualTo(dataDeNascimento));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromCliente_ShouldReturnCorrectDto()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var cliente = new Cliente
        {
            Id = clienteId,
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30),
            Usuario = new Usuario
            {
                Id = usuarioId,
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "11999999999",
                Perfil = PerfilUsuario.Cliente
            }
        };

        // Act
        UsuarioClienteOutputDto dto = cliente;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Id, Is.EqualTo(usuarioId));
            Assert.That(dto.ClientId, Is.EqualTo(clienteId));
            Assert.That(dto.Nome, Is.EqualTo(cliente.Usuario.Nome));
            Assert.That(dto.Email, Is.EqualTo(cliente.Usuario.Email));
            Assert.That(dto.Telefone, Is.EqualTo(cliente.Usuario.Telefone));
            Assert.That(dto.Cpf, Is.EqualTo(cliente.Cpf));
            Assert.That(dto.DataDeNascimento, Is.EqualTo(cliente.DataDeNascimento));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromClienteWithNullUsuario_ShouldThrowNullReferenceException()
    {
        // Arrange
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        // Act & Assert
        Assert.Throws<NullReferenceException>(() =>
        {
            UsuarioClienteOutputDto dto = cliente;
        });
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromClienteWithEmptyValues_ShouldReturnDtoWithEmptyValues()
    {
        // Arrange
        var cliente = new Cliente
        {
            Id = Guid.Empty,
            Cpf = "",
            DataDeNascimento = DateTime.MinValue,
            Usuario = new Usuario
            {
                Id = Guid.Empty,
                Nome = "",
                Email = "",
                Telefone = "",
                Perfil = PerfilUsuario.Cliente
            }
        };

        // Act
        UsuarioClienteOutputDto dto = cliente;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Id, Is.EqualTo(Guid.Empty));
            Assert.That(dto.ClientId, Is.EqualTo(Guid.Empty));
            Assert.That(dto.Nome, Is.EqualTo(""));
            Assert.That(dto.Email, Is.EqualTo(""));
            Assert.That(dto.Telefone, Is.EqualTo(""));
            Assert.That(dto.Cpf, Is.EqualTo(""));
            Assert.That(dto.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedFromClienteWithNullProperties_ShouldReturnDtoWithNullProperties()
    {
        // Arrange
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Cpf = string.Empty,
            DataDeNascimento = DateTime.Now.AddYears(-25),
            Usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = string.Empty,
                Email = string.Empty,
                Telefone = string.Empty,
                Perfil = PerfilUsuario.Cliente
            }
        };

        // Act
        UsuarioClienteOutputDto dto = cliente;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.Nome, Is.Empty);
            Assert.That(dto.Email, Is.Empty);
            Assert.That(dto.Telefone, Is.Empty);
            Assert.That(dto.Cpf, Is.Empty);
        }
    }

    [Test]
    public void ImplicitOperator_WhenConvertedMultipleTimes_ShouldReturnDifferentInstances()
    {
        // Arrange
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30),
            Usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = "João Silva",
                Email = "joao@email.com",
                Telefone = "11999999999",
                Perfil = PerfilUsuario.Cliente
            }
        };

        // Act
        UsuarioClienteOutputDto dto1 = cliente;
        UsuarioClienteOutputDto dto2 = cliente;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(dto1, Is.Not.Null);
            Assert.That(dto2, Is.Not.Null);
            Assert.That(dto1, Is.Not.SameAs(dto2));
            Assert.That(dto1.Id, Is.EqualTo(dto2.Id));
            Assert.That(dto1.ClientId, Is.EqualTo(dto2.ClientId));
            Assert.That(dto1.Nome, Is.EqualTo(dto2.Nome));
            Assert.That(dto1.Email, Is.EqualTo(dto2.Email));
            Assert.That(dto1.Telefone, Is.EqualTo(dto2.Telefone));
            Assert.That(dto1.Cpf, Is.EqualTo(dto2.Cpf));
            Assert.That(dto1.DataDeNascimento, Is.EqualTo(dto2.DataDeNascimento));
        }
    }
}