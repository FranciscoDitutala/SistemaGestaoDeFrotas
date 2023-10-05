
using Fleet.Transport.Data.Entities.Enums;

namespace Fleet.Transport.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string? DocumentNumber { get; set; }
        public  DocumentType Type { get; set; }
        public DateTime ExpiryDate { get; set; }
        public byte[]? FileContent { get; set; }
        public int VehicleId { get; set; }
        public int AssignmentID { get; set; }
    }
}
