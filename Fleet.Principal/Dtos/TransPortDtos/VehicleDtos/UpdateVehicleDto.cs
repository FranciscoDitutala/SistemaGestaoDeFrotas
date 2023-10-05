using Fleet.Transport;

namespace Fleet.Principal.Dtos.TransPortDtos.VehicleDtos
{
    public class UpdateVehicleDto
    {

        public int BrandId { get; set; }

        public int ModelId { get; set; }

        public int VariantId { get; set; }

        public int YearOfManufacture { get; set; }

        public VehicleType Type { get; set; }

        public TransmissionType Transmission { get; set; }

        public int Power { get; set; }

        public double FuelConsumption { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}
