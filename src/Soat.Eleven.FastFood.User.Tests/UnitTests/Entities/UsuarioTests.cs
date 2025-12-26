using Soat.Eleven.FastFood.User.Domain.Entities;
using Soat.Eleven.FastFood.User.Domain.Enums;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Entities;

[TestFixture]
public class UsuarioTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var usuario = new Usuario();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario, Is.Not.Null);
            Assert.That(usuario, Is.InstanceOf<IEntity>());
        }
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var nome = "João Silva";
        var email = "joao@email.com";
        var senha = "123456";
        var telefone = "11999999999";
        var perfil = PerfilUsuario.Cliente;
        var criadoEm = DateTime.Now;
        var modificadoEm = DateTime.Now.AddHours(1);
        var status = StatusUsuario.Ativo;

        // Act
        var usuario = new Usuario
        {
            Id = id,
            Nome = nome,
            Email = email,
            Senha = senha,
            Telefone = telefone,
            Perfil = perfil,
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm,
            Status = status
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Id, Is.EqualTo(id));
            Assert.That(usuario.Nome, Is.EqualTo(nome));
            Assert.That(usuario.Email, Is.EqualTo(email));
            Assert.That(usuario.Senha, Is.EqualTo(senha));
            Assert.That(usuario.Telefone, Is.EqualTo(telefone));
            Assert.That(usuario.Perfil, Is.EqualTo(perfil));
            Assert.That(usuario.CriadoEm, Is.EqualTo(criadoEm));
            Assert.That(usuario.ModificadoEm, Is.EqualTo(modificadoEm));
            Assert.That(usuario.Status, Is.EqualTo(status));
        }
    }

    [Test]
    public void Properties_WhenSetToEmptyStrings_ShouldReturnEmptyStrings()
    {
        // Act
        var usuario = new Usuario
        {
            Nome = "",
            Email = "",
            Senha = "",
            Telefone = ""
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Nome, Is.EqualTo(""));
            Assert.That(usuario.Email, Is.EqualTo(""));
            Assert.That(usuario.Senha, Is.EqualTo(""));
            Assert.That(usuario.Telefone, Is.EqualTo(""));
        }
    }

    [Test]
    public void Perfil_WhenSetToCliente_ShouldReturnCliente()
    {
        // Act
        var usuario = new Usuario
        {
            Perfil = PerfilUsuario.Cliente
        };

        // Assert
        Assert.That(usuario.Perfil, Is.EqualTo(PerfilUsuario.Cliente));
    }

    [Test]
    public void Perfil_WhenSetToAdministrador_ShouldReturnAdministrador()
    {
        // Act
        var usuario = new Usuario
        {
            Perfil = PerfilUsuario.Administrador
        };

        // Assert
        Assert.That(usuario.Perfil, Is.EqualTo(PerfilUsuario.Administrador));
    }

    [Test]
    public void Status_WhenSetToAtivo_ShouldReturnAtivo()
    {
        // Act
        var usuario = new Usuario
        {
            Status = StatusUsuario.Ativo
        };

        // Assert
        Assert.That(usuario.Status, Is.EqualTo(StatusUsuario.Ativo));
    }

    [Test]
    public void Status_WhenSetToInativo_ShouldReturnInativo()
    {
        // Act
        var usuario = new Usuario
        {
            Status = StatusUsuario.Inativo
        };

        // Assert
        Assert.That(usuario.Status, Is.EqualTo(StatusUsuario.Inativo));
    }

    [Test]
    public void CriarCliente_WhenCalled_ShouldCreateClienteWithCorrectValues()
    {
        // Arrange
        var usuario = new Usuario();
        var dataDeNascimento = DateTime.Now.AddYears(-30);
        var cpf = "12345678901";

        // Act
        usuario.CriarCliente(dataDeNascimento, cpf);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.Not.Null);
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(dataDeNascimento));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(cpf));
        }
    }

    [Test]
    public void CriarCliente_WhenCalledWithNullCpf_ShouldCreateClienteWithNullCpf()
    {
        // Arrange
        var usuario = new Usuario();
        var dataDeNascimento = DateTime.Now.AddYears(-25);

        // Act
        usuario.CriarCliente(dataDeNascimento, string.Empty);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.Not.Null);
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(dataDeNascimento));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(string.Empty));
        }
    }

    [Test]
    public void CriarCliente_WhenCalledWithEmptyCpf_ShouldCreateClienteWithEmptyCpf()
    {
        // Arrange
        var usuario = new Usuario();
        var dataDeNascimento = DateTime.Now.AddYears(-25);

        // Act
        usuario.CriarCliente(dataDeNascimento, "");

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.Not.Null);
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(dataDeNascimento));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(""));
        }
    }

    [Test]
    public void CriarCliente_WhenCalledWithDefaultDateTime_ShouldCreateClienteWithDefaultDateTime()
    {
        // Arrange
        var usuario = new Usuario();
        var cpf = "12345678901";

        // Act
        usuario.CriarCliente(default(DateTime), cpf);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.Not.Null);
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(default(DateTime)));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(cpf));
        }
    }

    [Test]
    public void CriarCliente_WhenCalledMultipleTimes_ShouldReplaceExistingCliente()
    {
        // Arrange
        var usuario = new Usuario();
        var firstDataNascimento = DateTime.Now.AddYears(-30);
        var firstCpf = "12345678901";
        var secondDataNascimento = DateTime.Now.AddYears(-25);
        var secondCpf = "98765432109";

        // Act
        usuario.CriarCliente(firstDataNascimento, firstCpf);
        var firstCliente = usuario.Cliente;
        
        usuario.CriarCliente(secondDataNascimento, secondCpf);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.Not.Null);
            Assert.That(usuario.Cliente, Is.Not.SameAs(firstCliente));
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(secondDataNascimento));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(secondCpf));
        }
    }

    [Test]
    public void CriarCliente_WhenCalledAfterSettingClienteManually_ShouldReplaceExistingCliente()
    {
        // Arrange
        var usuario = new Usuario();
        var existingCliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Cpf = "11111111111",
            DataDeNascimento = DateTime.Now.AddYears(-40)
        };
        usuario.Cliente = existingCliente;

        var newDataNascimento = DateTime.Now.AddYears(-25);
        var newCpf = "22222222222";

        // Act
        usuario.CriarCliente(newDataNascimento, newCpf);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.Not.Null);
            Assert.That(usuario.Cliente, Is.Not.SameAs(existingCliente));
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(newDataNascimento));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(newCpf));
        }
    }

    [Test]
    public void Cliente_WhenSetManually_ShouldReturnSetValue()
    {
        // Arrange
        var usuario = new Usuario();
        var cliente = new Cliente
        {
            Id = Guid.NewGuid(),
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        // Act
        usuario.Cliente = cliente;

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario.Cliente, Is.EqualTo(cliente));
            Assert.That(usuario.Cliente.Id, Is.EqualTo(cliente.Id));
            Assert.That(usuario.Cliente.Cpf, Is.EqualTo(cliente.Cpf));
            Assert.That(usuario.Cliente.DataDeNascimento, Is.EqualTo(cliente.DataDeNascimento));
        }
    }

    [Test]
    public void IEntity_ShouldImplementCorrectInterface()
    {
        // Arrange
        var usuario = new Usuario();

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(usuario, Is.AssignableTo<IEntity>());
            Assert.That(usuario.Id, Is.TypeOf<Guid>());
            Assert.That(usuario.CriadoEm, Is.TypeOf<DateTime>());
            Assert.That(usuario.ModificadoEm, Is.TypeOf<DateTime>());
        }
    }
}