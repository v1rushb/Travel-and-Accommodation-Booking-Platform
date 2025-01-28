namespace TABP.Domain.Abstractions.Services;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}