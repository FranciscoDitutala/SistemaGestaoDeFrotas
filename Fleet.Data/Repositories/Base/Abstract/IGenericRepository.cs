namespace Fleet.Data;

public interface IGenericRepository<T, K> where T : class where K : IComparable<K>
{
    Task<T?> FindAsync(Expression<Func<T, bool>>? expression, IList<string>? includes = null);

    IAsyncEnumerable<T> FilterAsync(
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        IList<string>? includes = null
    );

    Task<PagedModel<T>> FilterByPageAsync(int limit, int page,
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        IList<string>? includes = null);

    Task<T> InsertAsync(T entity);
    Task InsertRangeAsync(IEnumerable<T> entities);

    Task DeleteAsync(K key);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task<T> UpdateAsync(T entity);

    Task<int> CountAsync(Expression<Func<T, bool>>? expression = null);

    Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null);

}

