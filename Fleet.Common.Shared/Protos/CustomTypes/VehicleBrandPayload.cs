namespace Fleet.Common;

public partial class VehicleBrandPayload
{
    public byte[]? LogoData => Logo?.ToArray();
}
