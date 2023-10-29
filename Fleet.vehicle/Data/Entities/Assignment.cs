using Fleet.Transport.Data.Entities.Enums;

namespace Fleet.Transport.Data.Entities
{

    public class Assignment
    {
        public int Id { get; set; }
        public AssignmentType Type { get; set; }
        public  int VehicleId { get; set; }
        public int  OrgaoId { get; set; }
        public int EmployeeId { get; set; }
        public string? Description  { get; set; }
        public bool Active { get; set; } = true;
        public DateTime DateOfAssignment { get; set; }
        public DateTime EndDateOfAssignment { get; set; } 

        List<Document> Documents { get; set; }= new List<Document>();
    }
}
