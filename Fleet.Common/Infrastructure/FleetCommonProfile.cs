using Google.Protobuf;

namespace Fleet.Common.Infrastructure;

public class FleetCommonProfile : Profile
{
    public FleetCommonProfile()
    {
        CreateMap<VehicleBodySpecs, VehicleBodySpecs>();
        CreateMap<VehicleTechnicalSpecs, VehicleTechnicalSpecs>();

        CreateMap<VehicleBodySpecs, VehicleBodySpecsPayload>()
            .ReverseMap();
        CreateMap<VehicleTechnicalSpecs, VehicleTechnicalSpecsPayload>()
            .ReverseMap();

        CreateMap<VehicleBrand, VehicleBrand>();
        CreateMap<VehicleModel, VehicleModel>();
        CreateMap<VehicleVariant, VehicleVariant>();

        CreateMap<VehicleBrand, CreateVehicleBrandRequest>()
            .ReverseMap();
        CreateMap<VehicleBrand, VehicleBrandPayload>()
            .ReverseMap();

        CreateMap<VehicleModel, CreateVehicleModelRequest>()
            .ReverseMap();
        CreateMap<VehicleModel, VehicleModelPayload>()
            .ReverseMap();

        CreateMap<VehicleVariant, CreateVehicleVariantRequest>()
            .ReverseMap();
        CreateMap<VehicleVariant, VehicleVariantPayload>()
            .ReverseMap();

        CreateMap<byte[], ByteString>()
            .ConvertUsing(bytes => ByteString.CopyFrom(bytes));
        CreateMap<ByteString, byte[]>()
            .ConvertUsing(bs => bs.ToByteArray());
    }
}

