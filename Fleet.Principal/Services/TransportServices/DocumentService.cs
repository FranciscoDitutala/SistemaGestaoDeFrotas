using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.DocumentDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.TransportServices
{
    public class DocumentService : IDocumentService
    {
        private readonly DocumentManager.DocumentManagerClient _DocumentManagerClient;
        private readonly IMapper _mapper;

        public ObservableCollection<DocumentDto> documents { get; } = new();
        public DocumentService(DocumentManager.DocumentManagerClient DocumentManagerClient, IMapper mapper) {
            _DocumentManagerClient = DocumentManagerClient;
            _mapper = mapper;
        }
        private async Task<byte[]> ReadFileContent(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        public async Task<DocumentDto> AddDocumentAsync(DocumentDto document)
        {
         
      
        var ans  = await _DocumentManagerClient.AddDocumentAsync(_mapper.Map<TransportDocumentPayload>(document));

            return _mapper.Map<DocumentDto>(ans);
        }

        public async Task<DocumentDto> DeleteDocumentAsync(int id)
        {
            var ans = await _DocumentManagerClient.DeleteDocumentAsync(new DocumentRequest { Id=id});

            return _mapper.Map<DocumentDto>(ans);
        }

        public async Task<IEnumerable<DocumentDto>> FindAllDocumentByVehicleAsync(int vehicleId)
        {
            using var list= _DocumentManagerClient.FindAllDocumentByVehicle(new FindDocumentsByVehicleRequest { VehicleId = vehicleId });

            if(documents.Any() )
            {
                documents.Clear();
            }

            
            while(await list.ResponseStream.MoveNext())
            {
                var ans = list.ResponseStream.Current;

                documents.Add(_mapper.Map<DocumentDto>(ans));
            }
            
            return documents;

        }
        /*
        public async Task<IEnumerable<VehicleDocumentDto>> FindDocumentsVehicleByTypeAsync(DocumentType type)
        {
            using var list = _vehicleDocumentManagerClient.FindDocumentsVehicleByType(new FindDocumentsVehicleByTypeRequest {VehicleId=veh Type=type });

            if (documents.Any())
            {
                documents.Clear();
            }

            while (await list.ResponseStream.MoveNext())
            {
                var ans = list.ResponseStream.Current;
                documents.Add(_mapper.Map<VehicleDocumentDto>(ans));
            }

            return documents;
        }
        */
        public async Task<DocumentDto> UpdateDocumentAsync(int id, UpdateDocumentDto document)
        {
            var doc = await _DocumentManagerClient.FindDocumentAsync(new DocumentRequest { Id=id });
            var doc2 = _mapper.Map(document, doc);
            var ans = await _DocumentManagerClient.UpdateDocumentAsync(doc2);

            return _mapper.Map<DocumentDto>(ans);
        }

        public async Task<DocumentDto> FindDocumentAsync(int id)
        {
            var doc = await _DocumentManagerClient.FindDocumentAsync(new DocumentRequest { Id = id });
            return _mapper.Map<DocumentDto>(doc);
        }

        public async Task<IEnumerable<DocumentDto>> FindDocumentsByAssignmentsync(int assignmentId)
        {
            using var list = _DocumentManagerClient.FindAllDocumentByAssignment(new FindAllDocumentByAssignmentRequest{ AssignmentId=assignmentId });

            if (documents.Any())
            {
               documents.Clear();
            }


            while (await list.ResponseStream.MoveNext())
            {
                var ans = list.ResponseStream.Current;

                documents.Add(_mapper.Map<DocumentDto>(ans));
            }

            return documents;
        }

    }
}
