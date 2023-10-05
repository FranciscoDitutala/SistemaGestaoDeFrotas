using AutoMapper;
using Fleet.Common;
using Fleet.Principal.Dtos.CommonDtos.ModelDtos;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.CommonServices
{
    public class VehicleModelService : IVehicleModelService
    {

        private readonly VehicleModelManager.VehicleModelManagerClient _vehicleModelManagerClient;
        private readonly IMapper _mapper;
        public VehicleModelService(VehicleModelManager.VehicleModelManagerClient vehicleModelManagerClient, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleModelManagerClient = vehicleModelManagerClient;

        }
        public async Task<VehicleModelDto> CreateVehicleModelAsync(CreateVehicleModelDto createVehicleModelDto)
        {
            var create = _mapper.Map<CreateVehicleModelRequest>(createVehicleModelDto);

            var ans = await _vehicleModelManagerClient.CreateModelAsync(create);

            return _mapper.Map<VehicleModelDto>(ans);
        }

        public async Task<VehicleModelDto> DeleteVehicleModelAsync(int vehicleId)
        {
            var delete= await _vehicleModelManagerClient.DeleteVehicleModelAsync(new FindVehicleModelRequest { Id = vehicleId });
            return _mapper.Map<VehicleModelDto>(delete);

        }

        public Task<VehicleModelDto> disableVehicleModelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleModelDto> EnableVehicleModelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<VehicleModelDto> ModelsByBrand { get; } = new();
        public async Task<IEnumerable<VehicleModelDto>> GetAllVehicleModelsbyBrandAsync(int id, bool enabled)
        {
            using var list = _vehicleModelManagerClient.FindVehicleModelsByBrand(new FindVehicleModelsByBrandRequest { VehicleBrandId = id, OnlyEnabled = enabled});

            if(ModelsByBrand.Any())
                ModelsByBrand.Clear();

            while(await list.ResponseStream.MoveNext())
            {
                var model= list.ResponseStream.Current;
                ModelsByBrand.Add(_mapper.Map<VehicleModelDto>(model));
            }
            return ModelsByBrand;
        }

        public async Task<VehicleModelDto> GetVehicleModelAsync(int id)
        {
            var find = await _vehicleModelManagerClient.FindVehicleModelAsync(new FindVehicleModelRequest { Id = id });

            return _mapper.Map<VehicleModelDto>(find);
        }

        public async Task<VehicleModelDto> UpdateVehicleModelAsync(int id,UpdateVehicleModelDto vehicleModelDto)
        {

            var find = await _vehicleModelManagerClient.FindVehicleModelAsync(new FindVehicleModelRequest { Id = id });

            var Update = _mapper.Map(vehicleModelDto, find);

            var modelUpdate = await _vehicleModelManagerClient.UpdateModelAsync(Update);

            return _mapper.Map<VehicleModelDto>(modelUpdate);
        }
    }
}
