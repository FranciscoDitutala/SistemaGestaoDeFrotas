using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data
{
    [PrimaryKey(nameof(PartTypeName), nameof(PartCategoryName), nameof(SubCategory))]
    public class PartTypeCategory
    {
        [StringLength(127, MinimumLength = 1)]
        public string PartTypeName { get; set; } = default!;
        public PartType PartType { get; set; } = default!;

        [StringLength(127, MinimumLength = 1)]
        public string PartCategoryName { get; set; } = default!;
        public PartCategory PartCategory { get; set; } = default!;

        [Required]
        [StringLength(127)]
        public string SubCategory { get; set; } = default!;
    }
}
