using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace Fleet.Parts.Infrastructure;

public class FleetPartsProfile : Profile
{
	public FleetPartsProfile()
	{
        CreateMap<PartCategory, PartCategory>();
        CreateMap<PartCategory, PartCategoryPayload>()
            .ReverseMap();

        CreateMap<VehicleVariantFilter, VehicleVariantFilter>();
        CreateMap<VehicleVariantFilter, VehicleVariantFilterPayload>()
            .ReverseMap();

        CreateMap<Part, Part>();
        CreateMap<CreatePartRequest, Part>();
        CreateMap<Part, PartPayload>()
            .ReverseMap();

        CreateMap<DocumentMetadata, DocumentMetadata>();
        CreateMap<AddDocumentRequest, DocumentMetadata>();
        CreateMap<DocumentMetadata, DocumentPayload>();
        CreateMap<DocumentMetadata, DocumentMetadataPayload>()
            .ReverseMap();

        CreateMap<StockEntryLine, StockEntryLine>();
        CreateMap<StockEntryLine, StockEntryLinePayload>()
            .ReverseMap();

        CreateMap<StockEntry, StockEntry>();
        CreateMap<CreateStockEntryRequest, StockEntry>();
        CreateMap<UpdateStockEntryRequest, StockEntry>();
        CreateMap<StockEntry, StockEntryPayload>()
            .ReverseMap();

        CreateMap<StockOutLine, StockOutLine>();
        CreateMap<StockOutLine, StockOutLinePayload>()
            .ReverseMap();

        CreateMap<StockOut, StockOut>();
        CreateMap<CreateStockOutRequest, StockOut>();
        CreateMap<UpdateStockOutRequest, StockOut>();
        CreateMap<StockOut, StockOutPayload>()
            .ForMember(dest => dest.ApprovedBy, opt => opt.NullSubstitute(string.Empty))
            .ForMember(dest => dest.CancelledBy, opt => opt.NullSubstitute(string.Empty))
            .ForMember(dest => dest.DeliveredBy, opt => opt.NullSubstitute(string.Empty))
            .ForMember(dest => dest.DeliveredTo, opt => opt.NullSubstitute(string.Empty))
            .ReverseMap();

        CreateMap<byte[], ByteString>()
            .ConvertUsing(bytes => ByteString.CopyFrom(bytes));
        CreateMap<ByteString, byte[]>()
            .ConvertUsing(bs => bs.ToByteArray());

        CreateMap<DateTime, Timestamp>()
                .ConvertUsing(ts => Timestamp.FromDateTime(ts.ToUniversalTime()));
        CreateMap<Timestamp, DateTime>()
            .ConvertUsing(dt => dt.ToDateTime());
    }
}
