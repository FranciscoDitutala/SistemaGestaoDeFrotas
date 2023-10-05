using AutoMapper;

namespace Fleet.Management.Infrastructure
{
    public class FleetManagementProfile : Profile
    {
        public FleetManagementProfile()
        {

            
            CreateMap<VehicleBrandPayload, VehicleBrandPayload>();
            CreateMap<VehicleModelPayload, VehicleModelPayload>();
            CreateMap<VehicleVariantPayload, VehicleVariantPayload>();

            CreateMap<VehicleBrandPayload, CreateVehicleBrandRequest>();
            CreateMap<VehicleModelPayload, CreateVehicleModelRequest>();
            CreateMap<VehicleVariantPayload, CreateVehicleVariantRequest>();

            CreateMap<PartPayload, PartPayload>();
            CreateMap<StockEntryLinePayload, StockEntryLinePayload>();
            CreateMap<StockOutLinePayload, StockOutLinePayload>();

            CreateMap<StockEntryPayload, CreateStockEntryRequest>();
            CreateMap<StockEntryPayload, UpdateStockEntryRequest>();

            CreateMap<StockOutPayload, CreateStockOutRequest>();
            CreateMap<StockOutPayload, UpdateStockOutRequest>();

            CreateMap<PartPayload, CreatePartRequest>();
            
        }
    }
}
