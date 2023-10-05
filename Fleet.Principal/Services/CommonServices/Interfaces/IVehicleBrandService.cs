using Fleet.Principal.Dtos.CommonDtos.BrandDtos;

namespace Fleet.Principal.Services.CommonServices.Interfaces
{
    public interface IVehicleBrandService
    {

        public Task<IEnumerable<VehicleBrandDto>> GetVehicleBrandsAsync();
        public Task<VehicleBrandDto> GetVehicleBrandAsync(int id);
        public Task<VehicleBrandDto> CreateVehicleBrandAsync(VehicleBrandDto entity);
         public Task<VehicleBrandDto> DeleteVehicleBrandAsync(int id);
        public Task<VehicleBrandDto> EnableBrandAsync(int id);
        public Task<VehicleBrandDto> DisableBrandAsync(int id);
        public Task<VehicleBrandDto> UpdateBrandAsync(int id, VehicleBrandDto VehicleBrandDto);

        // public Task<VehicleBrand> GetVehicleBrandAsync(int id);

    }
}
