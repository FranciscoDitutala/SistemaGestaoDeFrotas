using System.ComponentModel.DataAnnotations;

namespace Fleet.Common.Data
{
    public class VehicleBrand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(127)]
        public string Company { get; set; } = default!;

        [Required]
        [StringLength(31)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(1_048_576)]
        public byte[] Logo { get; set; } = default!;

        public bool Enabled { get; set; }

        [MaxLength(512)]
        public List<VehicleModel> Models { get; set; } = new();
    }
}
