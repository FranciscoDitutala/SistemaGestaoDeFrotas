
using AutoMapper;
using Fleet.Transport.Data.Entities;
using Fleet.Transport.Data.Repositories;
using FleetException;
using Grpc.Core;

namespace Fleet.Transport.Services
{
    public class DocumentService: DocumentManager.DocumentManagerBase
    {

        private readonly DocumentRepository _DocumentRepository;
        private readonly IMapper _mapper;

        public DocumentService(DocumentRepository DocumentRepository, IMapper mapper) {
        
            _DocumentRepository =DocumentRepository;
            _mapper = mapper;
        
        }


        public override async Task<TransportDocumentPayload> AddDocument(TransportDocumentPayload request, ServerCallContext context)
        {
            var document = await _DocumentRepository.Entities.InsertAsync(_mapper.Map<Document>(request));
            await _DocumentRepository.SaveAsync();
            return _mapper.Map<TransportDocumentPayload>(document);
        }

        public override async  Task<TransportDocumentPayload> DeleteDocument(DocumentRequest request, ServerCallContext context)
        {
            var document =await _DocumentRepository.Entities.FindAsync(x=> x.Id ==request.Id);


            if (document == null)
            {
                throw new DomainException("o documento não existe");
            }

            await _DocumentRepository.Entities.DeleteAsync(request.Id);
            await _DocumentRepository.SaveAsync();

            return _mapper.Map<TransportDocumentPayload>(document);
        }

        public override async Task FindAllDocumentByVehicle(FindDocumentsByVehicleRequest request, IServerStreamWriter<TransportDocumentPayload> responseStream, ServerCallContext context)
        {
            await foreach (var document in _DocumentRepository.Entities.FilterAsync(x=> x.VehicleId == request.VehicleId))
            {
                await responseStream.WriteAsync(_mapper.Map<TransportDocumentPayload>(document));
            }
        }

        public override async Task<TransportDocumentPayload> UpdateDocument(TransportDocumentPayload request, ServerCallContext context)
        {
            var document = await _DocumentRepository.Entities.UpdateAsync(_mapper.Map<Document>(request));
            await _DocumentRepository.SaveAsync();

            return _mapper.Map<TransportDocumentPayload>(document);
        }

        public override async Task<TransportDocumentPayload> FindDocument(DocumentRequest request, ServerCallContext context)
        {
            var document = await _DocumentRepository.Entities.FindAsync(x=> x.Id == request.Id);

            return _mapper.Map<TransportDocumentPayload>(document);
        }

        public override async Task FindAllDocumentByAssignment(FindAllDocumentByAssignmentRequest request,
            IServerStreamWriter<TransportDocumentPayload> responseStream, ServerCallContext context)
        {
            await foreach (var document in _DocumentRepository.Entities.FilterAsync(x => x.AssignmentID== request.AssignmentId))
            {
                await responseStream.WriteAsync(_mapper.Map<TransportDocumentPayload>(document));
            }
        }

        public async Task DeleteAllDocumentById(int id)
        {
            var documents = _DocumentRepository.Entities.FilterAsync( x=> x.Id == id); 

            await foreach (var document in documents)
            {
                await _DocumentRepository.Entities.DeleteAsync(document.Id);
            }

            await _DocumentRepository.SaveAsync();

        }
        
    }
}
