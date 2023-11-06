using Fleet.Transport;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.TransPortDtos.VehicleDtos
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string? Vin { get; set; }
        public string? Registration { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public string? Cor { get; set; }
        public int YearOfManufacture { get; set; }

        public string? Type { get; set; }

        public string? Transmission { get; set; }

        public int Power { get; set; }

        public double FuelConsumption { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Assigned { get; set; }

    }
}
