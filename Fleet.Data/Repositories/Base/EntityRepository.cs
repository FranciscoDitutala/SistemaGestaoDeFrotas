namespace Fleet.Data;

public abstract class EntityRepository<T, K> : IEntityRepository<T, K> where T : class where K : IComparable<K>
{
    private bool disposedValue;
    private IGenericRepository<T, K> _entities;

    private readonly DbContext _context;

    public EntityRepository(DbContext context)
    {
        _context = context;
        _entities = new GenericRepository<T, K>(_context);
    }

    public IGenericRepository<T, K> Entities => _entities;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                _context.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~SavicopRepository()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

