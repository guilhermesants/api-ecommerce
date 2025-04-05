using Domain.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Concrets.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected EcommerceContext DbContext { get; set; }
    protected DbSet<T> DbSet { get; set; }

    public RepositoryBase(EcommerceContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext);

        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }

    public void Add(T entity) => DbSet.Add(entity);

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => DbSet.Where(predicate);

    public IQueryable<T> GetAll() => DbSet;

    public void Remove(T entity) => DbSet.Remove(entity);
}