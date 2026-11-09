using System.Linq.Expressions;
using HW2.Web.DAL.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HW2.Web.DAL.Concrete;

/// <summary>
/// Base repository for common CRUD operations.
/// </summary>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
{
    private readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public virtual TEntity? FindById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual bool Exists(int id)
    {
        return FindById(id) is not null;
    }

    public virtual IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }

    // Write operations save immediately instead of waiting for a separate unit of work.
    public virtual TEntity AddOrUpdate(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _context.Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public virtual void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Remove(entity);
        _context.SaveChanges();
    }

    public virtual void DeleteById(int id)
    {
        var entity = FindById(id)
            ?? throw new InvalidOperationException($"No {typeof(TEntity).Name} with ID {id} was found.");

        Delete(entity);
    }
}
