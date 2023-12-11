using AutoMapper;
using Fleet.Common;
using Fleet.Parts;
using Fleet.Principal.Dtos.CommonDtos.BrandDtos;
using Fleet.Principal.Dtos.CommonDtos.ModelDtos;
using Fleet.Principal.Dtos.CommonDtos.Owned;
using Fleet.Principal.Dtos.CommonDtos.VariantDtos;
using Fleet.Principal.Dtos.PartsDtos.Owned;
using Fleet.Principal.Dtos.PartsDtos.PartDtos;
using Fleet.Principal.Dtos.PartsDtos.StockOutDtos;
using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;
using Fleet.Principal.Dtos.TransPortDtos;
using Fleet.Principal.Dtos.TransPortDtos.AssignmentDtos;
using Fleet.Principal.Dtos.TransPortDtos.DocumentDtos;
using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Transport;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Fleet.Principal.Infrastructure
{
    public class FleetPrincipalProfile : Profile
    {
        public FleetPrincipalProfile()
        {

         // Fleet Comom Map

            // Brand Map
            CreateMap<VehicleBrandDto, VehicleBrandPayload>().ReverseMap();
            CreateMap<CreateVehicleBrandDto, VehicleBrandDto>();
            CreateMap<VehicleBrandDto, CreateVehicleBrandRequest>();
            CreateMap<UpdateVehicleBrandDto, VehicleBrandDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            // Model Map
            CreateMap<CreateVehicleModelDto, CreateVehicleModelRequest>();
            CreateMap<VehicleModelPayload,VehicleModelDto>().ReverseMap();
            CreateMap<VehicleModelPayload, UpdateVehicleModelDto>().ReverseMap();

            // Variant Map

            CreateMap<CreateVehicleVariantDto,CreateVehicleVariantRequest>().ReverseMap();
            CreateMap<VehicleVariantDto, VehicleVariantPayload>().ReverseMap();
            CreateMap<UpdateVehicleVariantDto, VehicleVariantPayload>().ReverseMap();

            CreateMap<VehicleBodySpecs, VehicleBodySpecs>();
            CreateMap<VehicleTechnicalSpecs, VehicleTechnicalSpecs>();

            CreateMap<VehicleBodySpecs, VehicleBodySpecsPayload>()
                .ReverseMap();
            CreateMap<VehicleTechnicalSpecs, VehicleTechnicalSpecsPayload>()
                .ReverseMap();
            //PartMapper
            CreateMap<PartPayload, PartDto>().ReverseMap();
            CreateMap<AddPartDto, CreatePartRequest>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreatePartRequest, AddPartDto>();
           

            CreateMap<PartCategoryPayload,PartCategoryDto>();
            CreateMap<PartTypePayload,PartTypeByCategoryDto>();
            CreateMap<PartTypePayload, PartTypeDto>();

            //StockyEntry
            CreateMap<StockEntryDto, StockEntryPayload>().ReverseMap();
            CreateMap<StockOutLine, StockOutLinePayload>()
            .ReverseMap();
            CreateMap<StockEntryLine, StockEntryLinePayload>()
            .ReverseMap();
            CreateMap<CreateStockEntryDto, CreateStockEntryRequest>();
            CreateMap<UpdateStockEntryDto, UpdateStockEntryRequest>();
            CreateMap<UpdateStockEntryDto, StockEntryPayload>();
            CreateMap<UpdateStockEntryRequest, StockEntryPayload>().ReverseMap();

            //Stockout
            CreateMap<StockOutDto, StockOutPayload>().ReverseMap();
            CreateMap<AddStockOutDto, CreateStockOutRequest>();
            CreateMap<UpdateStockOutDto, UpdateStockOutRequest>();
            CreateMap<UpdateStockOutDto, StockOutPayload>();
            CreateMap<UpdateStockOutRequest, StockOutPayload>().ReverseMap();

            //VehicleMapper
            CreateMap<VehicleDto, VehiclePayload>().ReverseMap();
                 CreateMap<VehicleDto, VehiclePayload>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AddVehicleDto, VehiclePayload>().ForMember(dest => dest.Assigned, opt => opt.MapFrom((src, dest, destMember, context) =>
                    false));
            CreateMap<UpdateVehicleDto, VehiclePayload>().ReverseMap();
            CreateMap<UpdateVehicleDto, VehiclePayload>()
               .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // VehicleDocument Mapper
            CreateMap<DocumentDto, TransportDocumentPayload>().ReverseMap();
            CreateMap<AddDocumentDto, DocumentDto>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AddDocumentDto, UpdateDocumentDto>()
             .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<UpdateDocumentDto, TransportDocumentPayload>()
                .ForMember(dest => dest.FileContent, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.FileContent != null ? ByteString.CopyFrom(src.FileContent) : destMember))
                .ForMember(dest => dest.ExpiryDate, opt => opt.Condition(src => (src.ExpiryDate.CompareTo(DateTime.Parse("0001-01-01T00:00:00Z"))!=0)))
                .ForMember(dest => dest.DocumentNumber, opt => opt.Condition((src, dest, srcMember) => srcMember != null))
                .ForMember(dest => dest.AssignmentId, opt => opt.Condition((src, dest, srcMember) => srcMember != 0))
                .ForMember(dest => dest.VehicleId, opt => opt.Condition((src, dest, srcMember) => srcMember != 0))
                .ForMember(dest => dest.Type, opt => opt.Condition((src, dest, srcMember) => srcMember != 0));

            // Assignment Mapper

            CreateMap<AssignmentDto, AssignmentPayload>().ReverseMap();
            CreateMap<AddAssignmentDto, AssignmentPayload>()
                .ForMember(dest => dest.DateOfAssignment, opt => opt.MapFrom((src, dest, destMember, context) =>
                    DateTime.Now
                ));
            CreateMap<UpdateAssignmentDto, AssignmentPayload>();
                //ForMember(dest => dest.Active, opt => opt.MapFrom((src, dest, destMember, context) => destMember));



            // Resto das configurações...


            // Resto das configurações...

            // Resto das configurações...



            //
            CreateMap<OrgaoDto, OrgaoPayload>().ReverseMap();
            CreateMap<EmployeeDto, EmployeePayload>().ReverseMap();

            //  Extra Mappers bytes
                CreateMap<IFormFile, byte[]>().ConvertUsing<ByteArrayTypeConverter>();
                CreateMap<byte[], ByteString>()
                    .ConvertUsing(bytes => ByteString.CopyFrom(bytes));
                 CreateMap<ByteString, byte[]>()
                .ConvertUsing(bs => bs.ToByteArray());
            CreateMap<DateTime, Timestamp>()
              .ConvertUsing(ts => Timestamp.FromDateTime(ts.ToUniversalTime()));
            CreateMap<Timestamp, DateTime>()
                .ConvertUsing(dt => dt.ToDateTime());

        }
        public class ByteArrayTypeConverter : ITypeConverter<IFormFile, byte[]>
        {
            public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
            {
                if (source == null || source.Length == 0)
                {
                    return null;
                }

                using var memoryStream = new MemoryStream();
                source.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
