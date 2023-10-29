using Fleet.Transport;

namespace Fleet.Principal.Dtos.TransPortDtos.VehicleDtos
{
    public class VehicleDetail
    {

        public string? Vin { get; set; }
        public string? Registration { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public string? Cor { get; set; }
        public int YearOfManufacture { get; set; }

        public VehicleType Type { get; set; }

        public TransmissionType Transmission { get; set; }

        public int Power { get; set; }

        public double FuelConsumption { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Assigned { get; set; }
        public AssignmentType TypeAssigment { get; set; }
        public string? Orgao { get; set; }
        public string? Employee { get; set; }
    }
}
