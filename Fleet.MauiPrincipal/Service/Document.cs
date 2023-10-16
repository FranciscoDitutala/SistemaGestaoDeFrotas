using Fleet.MauiPrincipal.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service
{
    public class Document
    {
        public int Id { get; set; }
        public string? DocumentNumber { get; set; }
        public DocumentType Type { get; set; }
        public DateTime ExpiryDate { get; set; }
        public byte[]? FileContent { get; set; }
        public int VehicleId { get; set; }
        public int AssignmentID { get; set; }
    }
}
