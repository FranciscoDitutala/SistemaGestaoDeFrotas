using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;

namespace Fleet.Principal.Services.TransportServices
{
    public class LocationService : ILocationService
    {

        private readonly LocationManager.LocationManagerClient _locationManagerClient;
        private readonly IMapper _mapper;

        public LocationService(LocationManager.LocationManagerClient locationManagerClient, IMapper mapper)
        {
            _locationManagerClient = locationManagerClient;
            _mapper = mapper;
        }

        public async Task<LocationDto> GetLocationById(int id)
        {
            var result = _locationManagerClient.FindLocationById(new FindLocationByIdRequest { VehicleId = id });

            return _mapper.Map<LocationDto> (result);
        }

        public async Task<LocationDto> GetLocationByRegistration(string registration)
        {
            var result = _locationManagerClient.FindLocationByRegistration(new FindLocationByRegistrationRequest { Registration=registration });

            return _mapper.Map<LocationDto>(result);
        }
    }
}
