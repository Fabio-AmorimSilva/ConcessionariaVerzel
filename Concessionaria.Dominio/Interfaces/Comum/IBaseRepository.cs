
using Concessionaria.Dominio.Core;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Concessionaria.Dominio.Interfaces.Comum
{
    public interface IBaseRepository<T> where T: Register
    {
        Task<T> Add(T model);
        Task<T> Update(T model);
        Task<T> Delete(int id);
        Task<T> GetById(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<IEnumerable<T>> GetAll(
          Expression<Func<T, bool>>? predicate = null,
          Func<IQueryable<T>,
          IIncludableQueryable<T, object>>? include = null,
          int? skip = null,
          int? take = null);
        Task<int> CountAll(Expression<Func<T, bool>>? predicate = null);
        IQueryable<T> FirstQuery();
    }
}
