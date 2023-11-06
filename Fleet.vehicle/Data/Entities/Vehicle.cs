using System.ComponentModel.DataAnnotations;

namespace Fleet.Transport.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        public string? Vin { get; set; }
        public string? Registration { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public string? Cor { get; set; }

        [Range(1900, 2099)]
        public int YearOfManufacture { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Transmission { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Power { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]
        public double FuelConsumption { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        public bool Assigned { get; set; }
        List<Document> Documents { get; set; }= new List<Document>();
        List<Assignment> Assignments { get; set; } = new List<Assignment>();

    }
}
