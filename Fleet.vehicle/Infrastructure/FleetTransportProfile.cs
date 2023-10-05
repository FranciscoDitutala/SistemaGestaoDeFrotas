using AutoMapper;
using Fleet.Transport.Data.Entities;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

namespace Fleet.Transport.Infrastructure
{
    public class FleetTransportProfile: Profile
    {
        public FleetTransportProfile() { 
            
            CreateMap<VehiclePayload,Vehicle>().ReverseMap();
            CreateMap<TransportDocumentPayload, Document>().ReverseMap();
            CreateMap<OrgaoPayload, Orgao>().ReverseMap();
            CreateMap<Employee,EmployeePayload>().ReverseMap();
            CreateMap<Assignment, AssignmentPayload>();
            CreateMap<AssignmentPayload, Assignment>().ForMember(dest => dest.EndDateOfAssignment, opt => opt.Ignore());
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
}
