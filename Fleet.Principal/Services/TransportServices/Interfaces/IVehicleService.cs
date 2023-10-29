using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Transport;

namespace Fleet.Principal.Services.TransportServices.Interfaces
{
    public interface IVehicleService
    {
        public Task<IEnumerable<VehicleDto>> FindAllVehiclesAsync();
        public Task<VehicleDto> FindVehicleAsync(int id);
        public Task<VehicleDto> AddVehicleAsync(AddVehicleDto vehicle);
        public Task<VehicleDto> UpdateVehicleAsync(int id,UpdateVehicleDto vehicle);
        public Task<VehicleDto> DeleteVehicleAsync(int id);
        public Task<IEnumerable<VehicleDto>> FindAllVehiclesByTypeAsync(VehicleType type);
        public Task<IEnumerable<VehicleDto>> FindAllVehiclesActiveAsync(bool active);
    }
}
