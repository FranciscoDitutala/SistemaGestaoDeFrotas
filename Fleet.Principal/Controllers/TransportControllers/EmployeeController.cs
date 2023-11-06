using AutoMapper;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{
    [ApiController]
    [Route("FleetTransport/[controller]")]
    public class EmployeeController : ControllerBase
    {
  
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getemployee(int id)
        {
            var ans = await _employeeService.FindEmployee(id);
            return Ok(ans);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var ans = await _employeeService.FindAllEmployees();
            return Ok(ans);
        }
        [HttpGet("getEmployesById/{orgaoId}")]
        public async Task<IActionResult> GetEmployesById(int orgaoId)
        {
            var ans = await _employeeService.FindEmployeeById(orgaoId);
            return Ok(ans);
        }

    }
}
