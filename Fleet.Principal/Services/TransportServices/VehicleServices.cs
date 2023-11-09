using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Grpc.Core;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Fleet.Principal.Services.TransportServices
{
    public class VehicleServices : IVehicleService
    {   

        private readonly VehicleManager.VehicleManagerClient _vehicleManagerClient;
        private readonly IMapper _mapper;

        public ObservableCollection<VehicleDto> Vehicles { get; } = new();
        public VehicleServices(VehicleManager.VehicleManagerClient vehicleManagerClient, IMapper mapper) {
            _vehicleManagerClient = vehicleManagerClient;
            _mapper = mapper;
        }
        public async Task<VehicleDto> AddVehicleAsync(AddVehicleDto vehicle)
        {
            var addvehicle = await _vehicleManagerClient.AddVehicleAsync(_mapper.Map<VehiclePayload>(vehicle));

            return _mapper.Map<VehicleDto>(addvehicle);
        }

        public async Task<VehicleDto> DeleteVehicleAsync(int id)
        {
            var delete = await _vehicleManagerClient.DeleteVehicleAsync(new FindVehicleRequest { Id = id });

            if (delete.Id <= 0) return new VehicleDto();
            return _mapper.Map<VehicleDto>(delete);
        }

       
        public async Task<IEnumerable<VehicleDto>> FindAllVehiclesAsync()
        {


            using var list =_vehicleManagerClient.FindAllVehicles(new FindAllVehiclesRequest());

            if(Vehicles.Any())
                Vehicles.Clear();

            while(await list.ResponseStream.MoveNext())
            {
                var vehicle = list.ResponseStream.Current;
                Vehicles.Add(_mapper.Map<VehicleDto>(vehicle));
            }

            return Vehicles;
        }

        public async Task<VehicleDto> FindVehicleAsync(int id)
        {

 
           var vehicle = await _vehicleManagerClient.FindVehicleAsync(new FindVehicleRequest { Id = id });

            if (vehicle.Id <= 0) return new VehicleDto();
               
           return _mapper.Map<VehicleDto>(vehicle);
        
           
        }

        public async Task<VehicleDto> UpdateVehicleAsync(int id,UpdateVehicleDto vehicle)
        {
            var upVehicle = await _vehicleManagerClient.FindVehicleAsync(new FindVehicleRequest { Id = id });

            var up = _mapper.Map(vehicle,upVehicle);

            var ans = await _vehicleManagerClient.UpdateVehicleAsync(up);

            return _mapper.Map<VehicleDto>(ans);

        }



     /*
        public async Task<IEnumerable<VehicleDto>> FindAllVehiclesByTypeAsync(string type)
        {
            using var list = _vehicleManagerClient.FindAllVehiclesByType(new FindByTypeRequest { Type = type });

            if (Vehicles.Any())
                Vehicles.Clear();

            while (await list.ResponseStream.MoveNext())
            {
                var vehicle = list.ResponseStream.Current;
                Vehicles.Add(_mapper.Map<VehicleDto>(vehicle));
            }
            return Vehicles;
        }
     */

        

       

        public async Task<IEnumerable<VehicleDto>> FindAllVehiclesActiveAsync(bool Assigned)
        {
            using var list = _vehicleManagerClient.FindAllVehiclesAssigned(new FindAllVehiclesAssignedRequest { Assigned =Assigned });

            if (Vehicles.Any())
                Vehicles.Clear();

            while (await list.ResponseStream.MoveNext())
            {
                var vehicle = list.ResponseStream.Current;
                Vehicles.Add(_mapper.Map<VehicleDto>(vehicle));
            }
            return Vehicles;
        }

        public async Task<IEnumerable<VehicleDto>> FindVehiclesAsync(string filter)
        {

            using var list = _vehicleManagerClient.FindVehicles(new FindVehiclesRequest { Filter = filter});

            if (Vehicles.Any())
                Vehicles.Clear();

            while (await list.ResponseStream.MoveNext())
            {
                var vehicle = list.ResponseStream.Current;
                Vehicles.Add(_mapper.Map<VehicleDto>(vehicle));
            }

            return Vehicles;
        }

        public async Task<bool> VerificarVinReg(AddVehicleDto vehicle)
        {
            using var list = _vehicleManagerClient.FindAllVehicles(new FindAllVehiclesRequest());

            while (await list.ResponseStream.MoveNext())
            {
                var ve = _mapper.Map<VehicleDto>(list.ResponseStream.Current);
                if (ve.Vin == vehicle.Vin || ve.Registration == vehicle.Registration)
                    return false;
            }

            return true;
        }
    }
}
