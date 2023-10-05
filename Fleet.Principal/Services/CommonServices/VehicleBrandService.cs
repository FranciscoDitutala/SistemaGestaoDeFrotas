using AutoMapper;
using Fleet.Common;
using Fleet.Principal.Dtos.CommonDtos.BrandDtos;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.CommonServices
{
    public class VehicleBrandService : IVehicleBrandService
    {


        private readonly VehicleBrandManager.VehicleBrandManagerClient _vehicleBrandManagerClient;
        private readonly IMapper _mapper;

        public VehicleBrandService(VehicleBrandManager.VehicleBrandManagerClient vehicleBrandManagerClient, IMapper mapper)
        {
            _vehicleBrandManagerClient = vehicleBrandManagerClient;
            _mapper = mapper;
        }

        public ObservableCollection<VehicleBrandDto> VehicleBrands { get; } = new();

        public async Task<IEnumerable<VehicleBrandDto>> GetVehicleBrandsAsync()
        {
            using var list = _vehicleBrandManagerClient.FindAllVehicleBrands(new FindAllVehicleBrandsRequest { OnlyEnabled = true });

            if(VehicleBrands.Any())
            {
                VehicleBrands.Clear();
            }
            while (await list.ResponseStream.MoveNext())
            {
                var brand = list.ResponseStream.Current;
                var vehicleBrand = _mapper.Map<VehicleBrandDto>(brand);
                VehicleBrands.Add(vehicleBrand);
            }
            return VehicleBrands;
        }
        public async Task<VehicleBrandDto> GetVehicleBrandAsync(int id)
        {


            var vehicle = await _vehicleBrandManagerClient.FindVehicleBrandAsync(new FindVehicleBrandRequest { Id = id });

            return _mapper.Map<VehicleBrandDto>(vehicle);
        }

        public async Task<VehicleBrandDto> CreateVehicleBrandAsync(VehicleBrandDto entity)
        {

            var vehicle = _mapper.Map<CreateVehicleBrandRequest>(entity);

            var create = await _vehicleBrandManagerClient.CreateBrandAsync(vehicle);

            var ans = _mapper.Map<VehicleBrandDto>(create);

            return ans;
        }
        public async Task<VehicleBrandDto> DeleteVehicleBrandAsync(int id)
        {
            var delete = await _vehicleBrandManagerClient.DeleteVehicleBrandAsync(new FindVehicleBrandRequest { Id = id });

            var ans = _mapper.Map<VehicleBrandDto>(delete);
            return ans;
        }
        public async Task<VehicleBrandDto> EnableBrandAsync(int id)
        {
            var enable = await _vehicleBrandManagerClient.EnableVehicleBrandAsync(new FindVehicleBrandRequest { Id = id });

            var ans = _mapper.Map<VehicleBrandDto>(enable);

            return ans;
        }

        public async Task<VehicleBrandDto> DisableBrandAsync(int id)
        {
            var desable = await _vehicleBrandManagerClient.DissableVehicleBrandAsync(new FindVehicleBrandRequest { Id = id });

            var ans = _mapper.Map<VehicleBrandDto>(desable);

            return ans;
        }

        public async Task<VehicleBrandDto> UpdateBrandAsync(int id, VehicleBrandDto VehicleBrandDto)
        {
            var vehicle = await _vehicleBrandManagerClient.FindVehicleBrandAsync(new FindVehicleBrandRequest { Id = id });

            var VehicleUpdate = _mapper.Map(VehicleBrandDto, vehicle);

            var ans = await _vehicleBrandManagerClient.UpdateBrandAsync(VehicleUpdate);

            return _mapper.Map<VehicleBrandDto>(ans);

        }

    }
}
