using System.Linq.Expressions;

namespace TaskManager.Core.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T?> FindByParam(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
