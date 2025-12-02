using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Entities;

[TestFixture]
public class ClienteTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var cliente = new Cliente();

        // Assert
        Assert.That(cliente, Is.Not.Null);
        Assert.That(cliente, Is.InstanceOf<IEntity>());
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cpf = "12345678901";
        var dataDeNascimento = DateTime.Now.AddYears(-30);
        var usuarioId = Guid.NewGuid();
        var criadoEm = DateTime.Now;
        var modificadoEm = DateTime.Now.AddHours(1);

        // Act
        var cliente = new Cliente
        {
            Id = id,
            Cpf = cpf,
            DataDeNascimento = dataDeNascimento,
            UsuarioId = usuarioId,
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm
        };

        // Assert
        Assert.That(cliente.Id, Is.EqualTo(id));
        Assert.That(cliente.Cpf, Is.EqualTo(cpf));
        Assert.That(cliente.DataDeNascimento, Is.EqualTo(dataDeNascimento));
        Assert.That(cliente.UsuarioId, Is.EqualTo(usuarioId));
        Assert.That(cliente.CriadoEm, Is.EqualTo(criadoEm));
        Assert.That(cliente.ModificadoEm, Is.EqualTo(modificadoEm));
    }

    [Test]
    public void Properties_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var cliente = new Cliente
        {
            Id = Guid.Empty,
            Cpf = null,
            UsuarioId = Guid.Empty,
            Usuario = null
        };

        // Assert
        Assert.That(cliente.Id, Is.EqualTo(Guid.Empty));
        Assert.That(cliente.Cpf, Is.Null);
        Assert.That(cliente.UsuarioId, Is.EqualTo(Guid.Empty));
        Assert.That(cliente.Usuario, Is.Null);
    }

    [Test]
    public void Properties_WhenSetToEmptyStrings_ShouldReturnEmptyStrings()
    {
        // Act
        var cliente = new Cliente
        {
            Cpf = ""
        };

        // Assert
        Assert.That(cliente.Cpf, Is.EqualTo(""));
    }

    [Test]
    public void DataDeNascimento_WhenSetToMinValue_ShouldReturnMinValue()
    {
        // Act
        var cliente = new Cliente
        {
            DataDeNascimento = DateTime.MinValue
        };

        // Assert
        Assert.That(cliente.DataDeNascimento, Is.EqualTo(DateTime.MinValue));
    }

    [Test]
    public void DataDeNascimento_WhenSetToMaxValue_ShouldReturnMaxValue()
    {
        // Act
        var cliente = new Cliente
        {
            DataDeNascimento = DateTime.MaxValue
        };

        // Assert
        Assert.That(cliente.DataDeNascimento, Is.EqualTo(DateTime.MaxValue));
    }

    [Test]
    public void DataDeNascimento_WhenSetToPastDate_ShouldReturnPastDate()
    {
        // Arrange
        var pastDate = DateTime.Now.AddYears(-25);

        // Act
        var cliente = new Cliente
        {
            DataDeNascimento = pastDate
        };

        // Assert
        Assert.That(cliente.DataDeNascimento, Is.EqualTo(pastDate));
    }

    [Test]
    public void DataDeNascimento_WhenSetToFutureDate_ShouldReturnFutureDate()
    {
        // Arrange
        var futureDate = DateTime.Now.AddYears(1);

        // Act
        var cliente = new Cliente
        {
            DataDeNascimento = futureDate
        };

        // Assert
        Assert.That(cliente.DataDeNascimento, Is.EqualTo(futureDate));
    }

    [Test]
    public void Cpf_WhenSetToValidLength_ShouldReturnCorrectValue()
    {
        // Arrange
        var cpf = "12345678901"; // 11 digits

        // Act
        var cliente = new Cliente
        {
            Cpf = cpf
        };

        // Assert
        Assert.That(cliente.Cpf, Is.EqualTo(cpf));
        Assert.That(cliente.Cpf.Length, Is.EqualTo(11));
    }

    [Test]
    public void Cpf_WhenSetToInvalidLength_ShouldReturnSetValue()
    {
        // Arrange
        var cpf = "123456789"; // 9 digits

        // Act
        var cliente = new Cliente
        {
            Cpf = cpf
        };

        // Assert
        Assert.That(cliente.Cpf, Is.EqualTo(cpf));
        Assert.That(cliente.Cpf.Length, Is.EqualTo(9));
    }

    [Test]
    public void Cpf_WhenSetWithSpecialCharacters_ShouldReturnSetValue()
    {
        // Arrange
        var cpf = "123.456.789-01";

        // Act
        var cliente = new Cliente
        {
            Cpf = cpf
        };

        // Assert
        Assert.That(cliente.Cpf, Is.EqualTo(cpf));
    }

    [Test]
    public void Usuario_WhenSetManually_ShouldReturnSetValue()
    {
        // Arrange
        var cliente = new Cliente();
        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = "João Silva",
            Email = "joao@email.com"
        };

        // Act
        cliente.Usuario = usuario;

        // Assert
        Assert.That(cliente.Usuario, Is.EqualTo(usuario));
        Assert.That(cliente.Usuario.Id, Is.EqualTo(usuario.Id));
        Assert.That(cliente.Usuario.Nome, Is.EqualTo(usuario.Nome));
        Assert.That(cliente.Usuario.Email, Is.EqualTo(usuario.Email));
    }

    [Test]
    public void UsuarioId_WhenSetToNewGuid_ShouldReturnCorrectGuid()
    {
        // Arrange
        var usuarioId = Guid.NewGuid();

        // Act
        var cliente = new Cliente
        {
            UsuarioId = usuarioId
        };

        // Assert
        Assert.That(cliente.UsuarioId, Is.EqualTo(usuarioId));
        Assert.That(cliente.UsuarioId, Is.Not.EqualTo(Guid.Empty));
    }

    [Test]
    public void CriadoEm_ModificadoEm_WhenSet_ShouldReturnCorrectDates()
    {
        // Arrange
        var criadoEm = DateTime.Now.AddDays(-1);
        var modificadoEm = DateTime.Now;

        // Act
        var cliente = new Cliente
        {
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm
        };

        // Assert
        Assert.That(cliente.CriadoEm, Is.EqualTo(criadoEm));
        Assert.That(cliente.ModificadoEm, Is.EqualTo(modificadoEm));
        Assert.That(cliente.ModificadoEm, Is.GreaterThan(cliente.CriadoEm));
    }

    [Test]
    public void IEntity_ShouldImplementCorrectInterface()
    {
        // Arrange
        var cliente = new Cliente();

        // Act & Assert
        Assert.That(cliente, Is.AssignableTo<IEntity>());
        Assert.That(cliente.Id, Is.TypeOf<Guid>());
        Assert.That(cliente.CriadoEm, Is.TypeOf<DateTime>());
        Assert.That(cliente.ModificadoEm, Is.TypeOf<DateTime>());
    }

    [Test]
    public void Cliente_WhenAllPropertiesSetTogether_ShouldMaintainConsistency()
    {
        // Arrange
        var id = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();
        var cpf = "98765432101";
        var dataDeNascimento = DateTime.Now.AddYears(-35);
        var criadoEm = DateTime.Now.AddDays(-5);
        var modificadoEm = DateTime.Now;
        var usuario = new Usuario
        {
            Id = usuarioId,
            Nome = "Maria Silva"
        };

        // Act
        var cliente = new Cliente
        {
            Id = id,
            UsuarioId = usuarioId,
            Cpf = cpf,
            DataDeNascimento = dataDeNascimento,
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm,
            Usuario = usuario
        };

        // Assert
        Assert.That(cliente.Id, Is.EqualTo(id));
        Assert.That(cliente.UsuarioId, Is.EqualTo(usuarioId));
        Assert.That(cliente.Cpf, Is.EqualTo(cpf));
        Assert.That(cliente.DataDeNascimento, Is.EqualTo(dataDeNascimento));
        Assert.That(cliente.CriadoEm, Is.EqualTo(criadoEm));
        Assert.That(cliente.ModificadoEm, Is.EqualTo(modificadoEm));
        Assert.That(cliente.Usuario, Is.EqualTo(usuario));
        Assert.That(cliente.Usuario.Id, Is.EqualTo(cliente.UsuarioId));
    }
}