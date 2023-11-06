using Fleet.Principal.Dtos.TransPortDtos;

namespace Fleet.Principal.Services.TransportServices.Interfaces
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDto>> FindAllEmployees();
        public Task<EmployeeDto> FindEmployee(int id);

        public Task<IEnumerable<EmployeeDto>> FindEmployeeById(int orgaoId);
    }
}
