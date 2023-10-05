using Fleet.Transport;

namespace Fleet.Principal.Dtos.TransPortDtos.DocumentDtos
{
    public class AddDocumentDto
    {
        public string? DocumentNumber { get; set; }
        public DocumentType Type { get; set; }
        public DateTime ExpiryDate { get; set; }
        public IFormFile? FileContent { get; set; }
        public int VehicleId { get; set; }
        public int AssignmentID { get; set; }
    }
}
