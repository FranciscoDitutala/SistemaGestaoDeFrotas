using AutoMapper;
using Fleet.Transport.Data.Repositories;
using Grpc.Core;

namespace Fleet.Transport.Services
{
    public class LocationService: LocationManager.LocationManagerBase
    {
        private readonly LocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(LocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public override async Task<LocationPayload> FindLocationById(FindLocationByIdRequest request, ServerCallContext context)
        {
            var result =  _locationRepository.FindLocationById(request.VehicleId);
            return _mapper.Map<LocationPayload>(result);
        }
        public override async Task<LocationPayload> FindLocationByRegistration(FindLocationByRegistrationRequest request, ServerCallContext context)
        {
            var result = _locationRepository.FindLocationByRegistration(request.Registration);
            return _mapper.Map<LocationPayload>(result);
        }

    }
}
