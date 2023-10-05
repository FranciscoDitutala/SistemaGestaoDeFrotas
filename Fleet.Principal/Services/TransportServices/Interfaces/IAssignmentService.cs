using Fleet.Principal.Dtos.TransPortDtos.AssignmentDtos;
using Fleet.Transport;

namespace Fleet.Principal.Services.TransportServices.Interfaces
{
    public interface IAssignmentService
    {

        public Task<IEnumerable<AssignmentDto>> FindAllAssignmentsByVehicleAsync(int vehicleId);
        public Task<AssignmentDto> FindAssignmentAsync(int id);
        public Task<AssignmentDto> AddAssignmentVehicleAsync(AddAssignmentDto addAssignmentDto);
        public Task<AssignmentDto> RemoveAssignmentVehicleAsync(int id);
        public Task<AssignmentDto> UpdateAssignmentVehicleAsync(int id,UpdateAssignmentDto updateAssigmentDto);

        public Task<AssignmentDto> FindAssignmentVehicleAsync(int vehicleId);

    }
}
