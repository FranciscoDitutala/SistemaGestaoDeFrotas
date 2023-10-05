using Fleet.Principal.Dtos.CommonDtos.VariantDtos;


namespace Fleet.Principal.Services.CommonServices.Interfaces
{
    public interface IVehicleVariantService
    {
        public Task<VehicleVariantDto> FindVehicleVariantAsync(int id);
        public Task<VehicleVariantDto> CreateVariantAsync(CreateVehicleVariantDto variant);

        public Task<VehicleVariantDto> UpdateVariantAsync(int id,UpdateVehicleVariantDto variant);

        public Task<VehicleVariantDto> EnableVehicleVariantAsync(int id);
        public Task<VehicleVariantDto> DissableVehicleVariantAsync(int id);
        public Task<VehicleVariantDto> DeleteVehicleVariantAsync(int id);
        public Task<IEnumerable<VehicleVariantDto>> FindVehicleVariantsByModelAsync(int modelId, bool enabled);

    }

}
