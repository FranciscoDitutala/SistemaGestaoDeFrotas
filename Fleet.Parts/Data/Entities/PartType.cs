using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data
{
    public class PartType
    {
        [Key]
        [StringLength(127, MinimumLength = 1)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(524_288)]
        public byte[] Image { get; set; } = default!;

        public List<Part> Parts { get; set; } = new();

        public ICollection<PartCategory> PartCategories { get; set; } = default!;
        public List<PartTypeCategory> PartTypeCategories { get; set; } = new();
    }
}
