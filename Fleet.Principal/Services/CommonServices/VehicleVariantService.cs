using AutoMapper;
using Fleet.Common;
using Fleet.Principal.Dtos.CommonDtos.VariantDtos;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Grpc.Core;
using System.Collections.ObjectModel;


namespace Fleet.Principal.Services.CommonServices
{
    public class VehicleVariantService : IVehicleVariantService
    {
        private readonly VehicleVariantManager.VehicleVariantManagerClient _vehicleVariantManagerClient;
        private readonly IMapper _mapper;

        public VehicleVariantService(VehicleVariantManager.VehicleVariantManagerClient vehicleVariantManagerClient,IMapper mapper) {
            _vehicleVariantManagerClient = vehicleVariantManagerClient;
            _mapper = mapper;
        }

        public async Task<VehicleVariantDto> CreateVariantAsync(CreateVehicleVariantDto variant)
        {
            var create= _mapper.Map<CreateVehicleVariantRequest>(variant);

            var created = await _vehicleVariantManagerClient.CreateVariantAsync(create);
           
            return _mapper.Map<VehicleVariantDto>(created);

         }

        public async Task<VehicleVariantDto> DeleteVehicleVariantAsync(int id)
        {
            var delete= await  _vehicleVariantManagerClient.DeleteVehicleVariantAsync( new FindVehicleVariantRequest{ Id = id } );

            return _mapper.Map<VehicleVariantDto>(delete);
        }

        public Task<VehicleVariantDto> DissableVehicleVariantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<VehicleVariantDto> EnableVehicleVariantAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<VehicleVariantDto> FindVehicleVariantAsync(int id)
        {
            var find = await _vehicleVariantManagerClient.FindVehicleVariantAsync(new FindVehicleVariantRequest { Id = id } );

            return _mapper.Map<VehicleVariantDto>(find);
           
        }

        public ObservableCollection<VehicleVariantDto> VehicleVariants { get; } = new ();
        public async Task<IEnumerable<VehicleVariantDto>> FindVehicleVariantsByModelAsync(int modelId, bool enabled)
        {
           using var list = _vehicleVariantManagerClient.FindVehicleVariantsByModel(new FindVehicleVariantsByModelRequest { VehicleModelId=modelId,OnlyEnabled = enabled } );

            if(VehicleVariants.Any())
                VehicleVariants.Clear();
            while( await list.ResponseStream.MoveNext())
            {
                var vehicleVariant = list.ResponseStream.Current;
                VehicleVariants.Add(_mapper.Map<VehicleVariantDto>(vehicleVariant));
            }
            return VehicleVariants;
        }
        public async Task<VehicleVariantDto> UpdateVariantAsync(int id,UpdateVehicleVariantDto variant)
        {

            var vehicleVariant =  await _vehicleVariantManagerClient.FindVehicleVariantAsync(new FindVehicleVariantRequest { Id = id });

             var update =_mapper.Map(variant, vehicleVariant);

            var ans = await _vehicleVariantManagerClient.UpdateVariantAsync(update);

            return _mapper.Map<VehicleVariantDto>(ans);
        }
    }
}
