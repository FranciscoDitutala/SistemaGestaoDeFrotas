using Fleet.Transport;

namespace Fleet.Principal.Dtos.TransPortDtos.AssignmentDtos
{
    public class AddAssignmentDto
    {
        public string? Type { get; set; }
        public int VehicleId { get; set; }
        public int OrgaoId { get; set; }
        public int EmployeeId { get; set; }
        public string? Description { get; set; }
    }
}
