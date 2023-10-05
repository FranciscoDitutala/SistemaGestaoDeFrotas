namespace Fleet.Data;

public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class where K : IComparable<K>
{
    private readonly DbContext _context;
    private readonly DbSet<T> _db;

    public GenericRepository(DbContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? expression = null)
    {
        IQueryable<T> query = FilterAndInclude(expression, null, null);
        return await query.CountAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>>? expression = null)
    {
        IQueryable<T> query = FilterAndInclude(expression, null, null);
        return await query.AnyAsync();
    }

    public async Task DeleteAsync(K key)
    {
        var entity = await _db.FindAsync(key);
        if(entity is not null)
            _db.Remove(entity);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        await Task.Run(() =>
        {
            _db.RemoveRange(entities);
        });
    }

    public IAsyncEnumerable<T> FilterAsync(
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        IList<string>? includes = null)
    {
        IQueryable<T> query = FilterAndInclude(expression, orderBy, includes);

        return query.AsAsyncEnumerable();
    }

    public async Task<PagedModel<T>> FilterByPageAsync(int limit, int page,
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        IList<string>? includes = null)
    {
        IQueryable<T> query = FilterAndInclude(expression, orderBy, includes);

        return await query.PaginateAsync(page, limit);
    }



    public async Task<T?> FindAsync(Expression<Func<T, bool>>? expression, IList<string>? includes = null)
    {
        IQueryable<T> query = FilterAndInclude(expression, null, includes);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<T> InsertAsync(T entity)
    {
        var result = await _db.AddAsync(entity);

        return result.Entity;
    }

    public async Task InsertRangeAsync(IEnumerable<T> entities)
    {
        await _db.AddRangeAsync(entities);
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        return await Task.Run(() =>
        {
            if (_db.Exists(entity))
                _db.Entry(entity).State = EntityState.Detached;

            return _db.Update(entity).Entity;
        });
        
    }

    #region UTILS
    private IQueryable<T> FilterAndInclude(
        Expression<Func<T, bool>>? expression,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
        IList<string>? includes)
    {
        IQueryable<T> query = _db;

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (includes is not null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return query;
        //return query.AsNoTracking();
    }

    // gylmar code
    
    #endregion
}

