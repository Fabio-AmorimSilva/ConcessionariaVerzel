
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Dominio.Models;
using Concessionaria.Infraestrutura.Context;
using Concessionaria.Infraestrutura.Repositories.Base;

namespace Concessionaria.Infraestrutura.Repositories
{
    public class UsuarioRepository<T> : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
