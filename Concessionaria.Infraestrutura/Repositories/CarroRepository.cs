
using Concessionaria.Dominio.Interfaces.Repositorios;
using Concessionaria.Dominio.Models;
using Concessionaria.Infraestrutura.Context;
using Concessionaria.Infraestrutura.Repositories.Base;

namespace Concessionaria.Infraestrutura.Repositories
{
    public class CarroRepository<T> : BaseRepository<Carro>, ICarroRepository
    {
        public CarroRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
