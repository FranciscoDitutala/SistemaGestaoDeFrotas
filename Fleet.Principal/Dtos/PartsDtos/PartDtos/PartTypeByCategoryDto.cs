
using Fleet.Principal.Dtos.PartsDtos.Owned;


namespace Fleet.Principal.Dtos.PartsDtos.PartDtos
{
    public class PartTypeByCategoryDto
    {

        public string PartTypeName { get; set; } = default!;
        public PartTypeDto PartType { get; set; } = default!;

        public string PartCategoryName { get; set; } = default!;
        public PartCategoryDto PartCategory { get; set; } = default!;

        public string SubCategory { get; set; } = default!;
    }
}
