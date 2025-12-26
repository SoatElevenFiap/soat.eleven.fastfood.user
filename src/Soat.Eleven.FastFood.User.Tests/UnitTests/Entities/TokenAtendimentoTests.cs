using Soat.Eleven.FastFood.User.Domain.Entities;

namespace Soat.Eleven.FastFood.User.Tests.UnitTests.Entities;

[TestFixture]
public class TokenAtendimentoTests
{
    [Test]
    public void Constructor_WhenCalled_ShouldCreateInstance()
    {
        // Act
        var token = new TokenAtendimento();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token, Is.Not.Null);
            Assert.That(token, Is.InstanceOf<IEntity>());
        }
    }

    [Test]
    public void Properties_WhenSet_ShouldReturnCorrectValues()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var criadoEm = DateTime.Now;
        var modificadoEm = DateTime.Now.AddHours(1);
        var cpf = "12345678901";

        // Act
        var token = new TokenAtendimento
        {
            Id = id,
            ClienteId = clienteId,
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm,
            Cpf = cpf
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(token.Id, Is.EqualTo(id));
            Assert.That(token.ClienteId, Is.EqualTo(clienteId));
            Assert.That(token.CriadoEm, Is.EqualTo(criadoEm));
            Assert.That(token.ModificadoEm, Is.EqualTo(modificadoEm));
            Assert.That(token.Cpf, Is.EqualTo(cpf));
        }
    }

    [Test]
    public void Properties_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var token = new TokenAtendimento
        {
            Id = Guid.Empty,
            ClienteId = null,
            Cpf = null,
            Cliente = null
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(token.Id, Is.EqualTo(Guid.Empty));
            Assert.That(token.ClienteId, Is.Null);
            Assert.That(token.Cpf, Is.Null);
            Assert.That(token.Cliente, Is.Null);
        }
    }

    [Test]
    public void Properties_WhenSetToEmptyStrings_ShouldReturnEmptyStrings()
    {
        // Act
        var token = new TokenAtendimento
        {
            Cpf = ""
        };

        // Assert
        Assert.That(token.Cpf, Is.EqualTo(""));
    }

    [Test]
    public void ClienteId_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var token = new TokenAtendimento
        {
            ClienteId = null
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token.ClienteId, Is.Null);
            Assert.That(token.ClienteId.HasValue, Is.False);
        }
    }

    [Test]
    public void ClienteId_WhenSetToValidGuid_ShouldReturnCorrectGuid()
    {
        // Arrange
        var clienteId = Guid.NewGuid();

        // Act
        var token = new TokenAtendimento
        {
            ClienteId = clienteId
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token.ClienteId, Is.EqualTo(clienteId));
            Assert.That(token.ClienteId.HasValue, Is.True);
            Assert.That(token.ClienteId.Value, Is.EqualTo(clienteId));
        }
    }

    [Test]
    public void ClienteId_WhenSetToEmptyGuid_ShouldReturnEmptyGuid()
    {
        // Act
        var token = new TokenAtendimento
        {
            ClienteId = Guid.Empty
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token.ClienteId, Is.EqualTo(Guid.Empty));
            Assert.That(token.ClienteId.HasValue, Is.True);
            Assert.That(token.ClienteId.Value, Is.EqualTo(Guid.Empty));
        }
    }

    [Test]
    public void Cpf_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var token = new TokenAtendimento
        {
            Cpf = null
        };

        // Assert
        Assert.That(token.Cpf, Is.Null);
    }

    [Test]
    public void Cpf_WhenSetToValidValue_ShouldReturnCorrectValue()
    {
        // Arrange
        var cpf = "12345678901";

        // Act
        var token = new TokenAtendimento
        {
            Cpf = cpf
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token.Cpf, Is.EqualTo(cpf));
            Assert.That(token.Cpf.Length, Is.EqualTo(11));
        }
    }

    [Test]
    public void Cpf_WhenSetToFormattedValue_ShouldReturnFormattedValue()
    {
        // Arrange
        var cpf = "123.456.789-01";

        // Act
        var token = new TokenAtendimento
        {
            Cpf = cpf
        };

        // Assert
        Assert.That(token.Cpf, Is.EqualTo(cpf));
    }

    [Test]
    public void Cliente_WhenSetToNull_ShouldReturnNull()
    {
        // Act
        var token = new TokenAtendimento
        {
            Cliente = null
        };

        // Assert
        Assert.That(token.Cliente, Is.Null);
    }

    [Test]
    public void Cliente_WhenSetToValidCliente_ShouldReturnCorrectCliente()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var cliente = new Cliente
        {
            Id = clienteId,
            Cpf = "12345678901",
            DataDeNascimento = DateTime.Now.AddYears(-30)
        };

        // Act
        var token = new TokenAtendimento
        {
            Cliente = cliente,
            ClienteId = clienteId
        };

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token.Cliente, Is.EqualTo(cliente));
            Assert.That(token.Cliente.Id, Is.EqualTo(clienteId));
            Assert.That(token.Cliente.Cpf, Is.EqualTo("12345678901"));
            Assert.That(token.ClienteId, Is.EqualTo(clienteId));
        }
    }

    [Test]
    public void CriadoEm_ModificadoEm_WhenSet_ShouldReturnCorrectDates()
    {
        // Arrange
        var criadoEm = DateTime.Now.AddDays(-1);
        var modificadoEm = DateTime.Now;

        // Act
        var token = new TokenAtendimento
        {
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(token.CriadoEm, Is.EqualTo(criadoEm));
            Assert.That(token.ModificadoEm, Is.EqualTo(modificadoEm));
            Assert.That(token.ModificadoEm, Is.GreaterThan(token.CriadoEm));
        }
    }

    [Test]
    public void IEntity_ShouldImplementCorrectInterface()
    {
        // Arrange
        var token = new TokenAtendimento();

        // Act & Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(token, Is.AssignableTo<IEntity>());
            Assert.That(token.Id, Is.TypeOf<Guid>());
            Assert.That(token.CriadoEm, Is.TypeOf<DateTime>());
            Assert.That(token.ModificadoEm, Is.TypeOf<DateTime>());
        }
    }

    [Test]
    public void TokenAtendimento_WhenAllPropertiesSetTogether_ShouldMaintainConsistency()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clienteId = Guid.NewGuid();
        var cpf = "98765432101";
        var criadoEm = DateTime.Now.AddDays(-5);
        var modificadoEm = DateTime.Now;
        var cliente = new Cliente
        {
            Id = clienteId,
            Cpf = cpf,
            DataDeNascimento = DateTime.Now.AddYears(-25)
        };

        // Act
        var token = new TokenAtendimento
        {
            Id = id,
            ClienteId = clienteId,
            Cpf = cpf,
            CriadoEm = criadoEm,
            ModificadoEm = modificadoEm,
            Cliente = cliente
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(token.Id, Is.EqualTo(id));
            Assert.That(token.ClienteId, Is.EqualTo(clienteId));
            Assert.That(token.Cpf, Is.EqualTo(cpf));
            Assert.That(token.CriadoEm, Is.EqualTo(criadoEm));
            Assert.That(token.ModificadoEm, Is.EqualTo(modificadoEm));
            Assert.That(token.Cliente, Is.EqualTo(cliente));
            Assert.That(token.Cliente.Id, Is.EqualTo(token.ClienteId));
            Assert.That(token.Cliente.Cpf, Is.EqualTo(token.Cpf));
        }
    }

    [Test]
    public void TokenAtendimento_WhenClienteIdAndCpfDontMatch_ShouldAllowInconsistency()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var tokenCpf = "11111111111";
        var cliente = new Cliente
        {
            Id = clienteId,
            Cpf = "22222222222" // Different CPF
        };

        // Act
        var token = new TokenAtendimento
        {
            ClienteId = clienteId,
            Cpf = tokenCpf,
            Cliente = cliente
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert - Should allow inconsistency as it's not enforced at entity level
            Assert.That(token.ClienteId, Is.EqualTo(clienteId));
            Assert.That(token.Cpf, Is.EqualTo(tokenCpf));
            Assert.That(token.Cliente.Cpf, Is.EqualTo("22222222222"));
            Assert.That(token.Cpf, Is.Not.EqualTo(token.Cliente.Cpf));
        }
    }

    [Test]
    public void TokenAtendimento_WhenUsingWithoutCliente_ShouldWorkWithJustCpf()
    {
        // Arrange
        var cpf = "12345678901";

        // Act
        var token = new TokenAtendimento
        {
            Id = Guid.NewGuid(),
            ClienteId = null,
            Cpf = cpf,
            Cliente = null
        };

        using (Assert.EnterMultipleScope())
        {
            // Assert
            Assert.That(token.ClienteId, Is.Null);
            Assert.That(token.Cpf, Is.EqualTo(cpf));
            Assert.That(token.Cliente, Is.Null);
        }
    }
}