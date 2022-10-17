using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MozaeekCore.Domain
{
    public interface IGenericRepository<T>
    {
        Task Add(T entity);
        Task AddRange(List<T> entities);
        void Update(T entity);

        void Delete(T entity);
        void ReSeed();
        void DeleteAll();
        Task<T> Find(long id);

        IQueryable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
    }
}