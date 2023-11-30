namespace WebProgrammingTerm.Auth.Core.DTOs;

public interface IUnitOfWork
{
    Task CommitAsync();
    void Commit();
}