using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Grpc.Core;
using System.Collections.ObjectModel;
using static Fleet.Transport.OrgaoManager;

namespace Fleet.Principal.Services.TransportServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeManager.EmployeeManagerClient _employeeManagerClient;
        private readonly IMapper _mapper;

        public ObservableCollection<EmployeeDto> employees { get; } = new ObservableCollection<EmployeeDto>();

        public EmployeeService(EmployeeManager.EmployeeManagerClient employeeManagerClient, IMapper mapper)
        {
            _employeeManagerClient = employeeManagerClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> FindAllEmployees()
        {
            using var list = _employeeManagerClient.FindAllEmployees(new FindAllEmployeesRequest());

            if (employees.Any())
                employees.Clear();

            while (await list.ResponseStream.MoveNext())
            {
                var orgao = list.ResponseStream.Current;
                employees.Add(_mapper.Map<EmployeeDto>(orgao));
            }

            return employees;
        }

        public async Task<EmployeeDto> FindEmployee(int id)
        {
            var employee = await _employeeManagerClient.FindEmployeeAsync(new FindEmployeeRequest { Id = id });

            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
