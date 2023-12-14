using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;

namespace Fleet.Principal.Services.TransportServices.Interfaces
{
    public interface ILocationService
    {
        public Task<LocationDto> GetLocationById(int id);
        public Task<LocationDto> GetLocationByRegistration(string registration);

    }
}
