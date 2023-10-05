using AutoMapper;

namespace Fleet.Management.Infrastructure
{
    public class FleetManagementProfile : Profile
    {
        public FleetManagementProfile()
        {
            CreateMap<VehicleBrandPayload, CreateVehicleBrandRequest>();
            CreateMap<VehicleModelPayload, CreateVehicleModelRequest>();
            CreateMap<VehicleVariantPayload, CreateVehicleVariantRequest>();
        }
    }
}
