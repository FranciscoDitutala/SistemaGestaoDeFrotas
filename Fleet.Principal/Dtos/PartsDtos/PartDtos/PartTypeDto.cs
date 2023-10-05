using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.PartsDtos.PartDtos
{
    public class PartTypeDto
    {
        public string Name { get; set; } = default!;

        public string SubCategory { get; set; } = default!;

        public byte[] Image { get; set; } = default!;

    }
}
