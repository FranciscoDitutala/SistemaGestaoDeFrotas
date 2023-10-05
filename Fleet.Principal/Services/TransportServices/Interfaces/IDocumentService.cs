using Fleet.Principal.Dtos.TransPortDtos.DocumentDtos;
using Fleet.Transport;

namespace Fleet.Principal.Services.TransportServices.Interfaces
{
    public interface IDocumentService
    {

        public Task<DocumentDto> FindDocumentAsync(int id);
        public Task<IEnumerable<DocumentDto>> FindAllDocumentByVehicleAsync(int vehicleId);
        public Task<IEnumerable<DocumentDto>> FindDocumentsByAssignmentsync(int assignmentId);
        public Task<DocumentDto> AddDocumentAsync(DocumentDto document);
        public Task<DocumentDto> UpdateDocumentAsync(int id, UpdateDocumentDto document);
        public Task<DocumentDto> DeleteDocumentAsync(int id);
        
    }
}
