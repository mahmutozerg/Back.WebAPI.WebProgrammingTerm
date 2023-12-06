namespace WebProgrammingTerm.Core.UnitOfWorks;

public interface IUnitOfWork
{
    Task CommitAsync();
    void Commit();
}