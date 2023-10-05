

namespace Fleet.Parts;

public partial class PartCategoryPayload
{
    public byte[]? ImageData => Image?.ToArray(); 
}
 