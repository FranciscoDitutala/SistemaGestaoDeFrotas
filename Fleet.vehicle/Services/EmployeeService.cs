using AutoMapper;
using Fleet.Transport.Data.Entities;
using Fleet.Transport.Data.Repositories;
using Grpc.Core;

namespace Fleet.Transport.Services
{
    public class EmployeeService:EmployeeManager.EmployeeManagerBase
    {

        private readonly EmployeeRepository _repository;
        private readonly IMapper _mapper;
        public EmployeeService(EmployeeRepository employeeRepository, IMapper mapper)
        {

            _repository = employeeRepository;
            _mapper = mapper;

        }

        public override async Task FindAllEmployees(FindAllEmployeesRequest request, IServerStreamWriter<EmployeePayload> responseStream, ServerCallContext context)
        {
            var employees = _repository.Employees;

            foreach (var employee in employees)
            {
                await responseStream.WriteAsync(_mapper.Map<EmployeePayload>(employee));
            }
        }

        public override async Task<EmployeePayload> FindEmployee(FindEmployeeRequest request, ServerCallContext context)
        {
            var employee = _repository.Find(request.Id);

            if (employee.Id <= 0) return new EmployeePayload();
             return _mapper.Map<EmployeePayload>(employee);

        }
        public override async Task FindAllEmployeesbyId(FindEmployeesRequest request, IServerStreamWriter<EmployeePayload> responseStream, ServerCallContext context)
        {
            var employees = _repository.Employees;

            foreach (var employee in employees)
            {

                if(employee.OrgaoId != request.OrgaoId) continue;
                await responseStream.WriteAsync(_mapper.Map<EmployeePayload>(employee));
            }
        }
    }
}
