namespace Fleet.Data;

public interface IEntityRepository<T, K> : IDisposable where T : class where K : IComparable<K>
{
    IGenericRepository<T, K> Entities { get; }

    Task SaveAsync();
}

