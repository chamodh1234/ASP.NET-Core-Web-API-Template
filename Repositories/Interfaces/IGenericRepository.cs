using System.Linq.Expressions;
using WebApiTemplate.Models.Entities;

namespace WebApiTemplate.Repositories.Interfaces;

/// <summary>
/// Generic repository interface for common CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity or null if not found</returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Get entity by ID with includes
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <param name="includes">Navigation properties to include</param>
    /// <returns>Entity or null if not found</returns>
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get all entities with includes
    /// </summary>
    /// <param name="includes">Navigation properties to include</param>
    /// <returns>List of all entities</returns>
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Find entities by predicate
    /// </summary>
    /// <param name="predicate">Filter predicate</param>
    /// <returns>List of matching entities</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Find entities by predicate with includes
    /// </summary>
    /// <param name="predicate">Filter predicate</param>
    /// <param name="includes">Navigation properties to include</param>
    /// <returns>List of matching entities</returns>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Get single entity by predicate
    /// </summary>
    /// <param name="predicate">Filter predicate</param>
    /// <returns>Entity or null if not found</returns>
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get single entity by predicate with includes
    /// </summary>
    /// <param name="predicate">Filter predicate</param>
    /// <param name="includes">Navigation properties to include</param>
    /// <returns>Entity or null if not found</returns>
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Add new entity
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>Added entity</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Add multiple entities
    /// </summary>
    /// <param name="entities">Entities to add</param>
    /// <returns>Added entities</returns>
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity">Entity to update</param>
    void Update(T entity);

    /// <summary>
    /// Update multiple entities
    /// </summary>
    /// <param name="entities">Entities to update</param>
    void UpdateRange(IEnumerable<T> entities);

    /// <summary>
    /// Remove entity
    /// </summary>
    /// <param name="entity">Entity to remove</param>
    void Remove(T entity);

    /// <summary>
    /// Remove entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    Task RemoveAsync(int id);

    /// <summary>
    /// Remove multiple entities
    /// </summary>
    /// <param name="entities">Entities to remove</param>
    void RemoveRange(IEnumerable<T> entities);

    /// <summary>
    /// Check if entity exists
    /// </summary>
    /// <param name="predicate">Filter predicate</param>
    /// <returns>True if entity exists</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get count of entities
    /// </summary>
    /// <param name="predicate">Filter predicate (optional)</param>
    /// <returns>Count of entities</returns>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    /// <summary>
    /// Get paginated entities
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <param name="predicate">Filter predicate (optional)</param>
    /// <param name="orderBy">Order by expression (optional)</param>
    /// <param name="includes">Navigation properties to include</param>
    /// <returns>Paginated result</returns>
    Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? predicate = null,
        Expression<Func<T, object>>? orderBy = null,
        params Expression<Func<T, object>>[] includes);
} 