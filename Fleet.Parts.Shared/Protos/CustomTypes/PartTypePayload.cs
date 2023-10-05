namespace Fleet.Parts;

public partial class PartTypePayload
{
    public byte[]? ImageData => Image?.ToArray();
}
