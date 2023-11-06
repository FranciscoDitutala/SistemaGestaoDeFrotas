using Fleet.Transport;

namespace Fleet.Principal.Dtos.TransPortDtos.AssignmentDtos
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public  int VehicleId { get; set; }
        public int  OrgaoId { get; set; }
        public int EmployeeId { get; set; }
        public string? Description  { get; set; }
        public bool Active { get; set; } = true;
        public DateTime DateOfAssignment { get; set; }
        public DateTime EndDateOfAssignment { get; set; }

    }
}
