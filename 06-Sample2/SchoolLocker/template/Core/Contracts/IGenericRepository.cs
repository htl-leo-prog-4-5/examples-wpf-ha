using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Linq.Expressions;

namespace Core.Contracts;

public interface IGenericRepository<TEntity> where TEntity : class, IEntityObject, new()
{
    /// <summary>
    ///     Eindeutige Entität oder null zurückliefern
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<TEntity?> GetByIdAsync(int id, params string[] includeProperties);


    /// <summary>
    /// Fügt neue Identität zum Datenbestand hinzu
    /// </summary>
    /// <param name="entity"></param>
    Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

    /// <summary>
    ///     Liste von Entities einfügen
    /// </summary>
    /// <param name="entities"></param>
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Generisches CountAsync mit Filtermöglichkeit. Sind vom Filter
    ///     Navigationproperties betroffen, können diese per eager-loading
    ///     geladen werden.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
}