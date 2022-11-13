
namespace Concessionaria.Dominio.Interfaces.Comum
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
