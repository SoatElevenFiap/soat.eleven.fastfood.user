namespace Soat.Eleven.FastFood.User.Domain.Entities;

public interface IEntity
{
    Guid Id { get; set; }
    DateTime CriadoEm { get; set; }
    DateTime ModificadoEm { get; set; }
}
