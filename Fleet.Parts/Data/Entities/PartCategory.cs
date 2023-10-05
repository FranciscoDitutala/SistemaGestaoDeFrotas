using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data
{
    public class PartCategory
    {
        [Key]
        [StringLength(127, MinimumLength = 1)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(524_288)]
        public byte[] Image { get; set; } = default!;

        public ICollection<PartType> PartTypes { get; set; } = default!;
        public List<PartTypeCategory> PartTypeCategories { get; set; } = new();
    }
}
