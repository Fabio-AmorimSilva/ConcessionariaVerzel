
using Concessionaria.Dominio.Interfaces.Comum;
using Concessionaria.Infraestrutura.Context;

namespace Concessionaria.Infraestrutura.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext _context { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
