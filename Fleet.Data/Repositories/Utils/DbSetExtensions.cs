namespace Fleet.Data;

public static class DbSetExtensions
{
    public static bool Exists<TEntity>(this DbSet<TEntity> db, TEntity entity)
        where TEntity : class
    {
        return db.Local.Any(e => e.Equals(entity));
    }
}

