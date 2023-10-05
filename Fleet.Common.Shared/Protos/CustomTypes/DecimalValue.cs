namespace Fleet.Common;

public partial class DecimalValue
{
    private const decimal NanoFactor = 1_000_000_000;
    public DecimalValue(long units, int nanos) : this()
    {
        Units = units;
        Nanos = nanos;
    }

    static decimal ToDecimal(DecimalValue grpcDecimal) => grpcDecimal is null ? 0.0m : grpcDecimal.Units + grpcDecimal.Nanos / NanoFactor;
    static DecimalValue FromDecimal(decimal value)
    {
        var units = decimal.ToInt64(value);
        var nanos = decimal.ToInt32((value - units) * NanoFactor);
        return new DecimalValue(units, nanos);
    }

    public static implicit operator decimal?(DecimalValue grpcDecimal) => grpcDecimal is null ? null : ToDecimal(grpcDecimal);

    public static implicit operator decimal(DecimalValue grpcDecimal) => ToDecimal(grpcDecimal);

    public static implicit operator DecimalValue(decimal? value) => FromDecimal(value ?? 0.0m);

    public static implicit operator DecimalValue(decimal value) => FromDecimal(value);
}

