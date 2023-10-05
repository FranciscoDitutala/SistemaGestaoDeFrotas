using Fleet.Principal.Dtos.CommonDtos.ModelDtos;

namespace Fleet.Principal.Services.CommonServices.Interfaces
{
    public interface IVehicleModelService
    {

        public Task<VehicleModelDto> GetVehicleModelAsync(int id);
        public Task<VehicleModelDto> CreateVehicleModelAsync(CreateVehicleModelDto createVehicleModelDto);
        public Task<VehicleModelDto> UpdateVehicleModelAsync(int id,UpdateVehicleModelDto vehicleModelDto);
        public Task<VehicleModelDto> EnableVehicleModelAsync(int id);
        public Task<VehicleModelDto> disableVehicleModelAsync(int id);

        public Task<VehicleModelDto> DeleteVehicleModelAsync(int vehicleId);

        public Task<IEnumerable<VehicleModelDto>> GetAllVehicleModelsbyBrandAsync(int id, bool enabled);

    }
}
