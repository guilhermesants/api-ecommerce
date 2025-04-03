using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories;

public interface IRepositoryBase<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    void Remove(T entity);
    void Add(T entity);
}