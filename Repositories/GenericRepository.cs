using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiTemplate.Data;
using WebApiTemplate.Models.Entities;
using WebApiTemplate.Repositories.Interfaces;

namespace WebApiTemplate.Repositories;

/// <summary>
/// Generic repository implementation
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.Where(e => !e.IsDeleted).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).Where(e => !e.IsDeleted).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.Where(predicate).Where(e => !e.IsDeleted).ToListAsync();
    }

    public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(e => !e.IsDeleted).SingleOrDefaultAsync(predicate);
    }

    public virtual async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.Where(e => !e.IsDeleted).SingleOrDefaultAsync(predicate);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        var entityList = entities.ToList();
        await _dbSet.AddRangeAsync(entityList);
        return entityList;
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Remove(T entity)
    {
        // Soft delete
        entity.IsDeleted = true;
        entity.UpdatedAt = DateTime.UtcNow;
        _dbSet.Update(entity);
    }

    public virtual async Task RemoveAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            Remove(entity);
        }
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            Remove(entity);
        }
    }

    public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(e => !e.IsDeleted).AnyAsync(predicate);
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        var query = _dbSet.Where(e => !e.IsDeleted);
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        return await query.CountAsync();
    }

    public virtual async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>? orderBy = null,
        params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();

        // Apply includes
        query = includes.Aggregate(query, (current, include) => current.Include(include));

        // Apply predicate (filter)
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Apply soft delete filter
        query = query.Where(e => !e.IsDeleted);

        // Get total count
        var totalCount = await query.CountAsync();

        // Apply ordering
        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }
        else
        {
            // Default ordering by ID
            query = query.OrderBy(e => e.Id);
        }

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
} 