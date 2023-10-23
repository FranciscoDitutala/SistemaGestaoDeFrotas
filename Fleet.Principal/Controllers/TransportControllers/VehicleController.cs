using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.DocumentDtos;
using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Principal.Services.TransportServices;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{
    [ApiController]
    [Route("FleetTransport/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, 
            IOrgaoService orgaoService, IEmployeeService employeeService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {

            var ans = await _vehicleService.FindVehicleAsync(id);
            return Ok(ans);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {

            var ans = await _vehicleService.FindAllVehiclesAsync();
            return Ok(ans);
        }

       
        [HttpGet("GetAllVehiclesbyType/{type}")]
        public async Task<IActionResult> GetAllVehiclesByType(int type)
        {
            var ans = await _vehicleService.FindAllVehiclesByTypeAsync((VehicleType) type);

            return Ok(ans);
        }
        [HttpGet("GetAllVehiclesbyOrgao/{orgaoId}")]
        public async Task<IActionResult> GetAllVehiclesByOrgao(int orgaoId)
        {
            var ans = await _vehicleService.FindAllVehiclesByOrgaoAsync(orgaoId);

            return Ok(ans);
        }

        [HttpGet("GetAllVehiclesbyEmployee/{employeeId}")]
        public async Task<IActionResult> GetAllVehiclesByEmployee(int employeeId)
        {
            var ans = await _vehicleService.FindAllVehiclesByEmployeeAsync(employeeId);

            return Ok(ans);
        }

        [HttpGet("GetAllVehiclesAssigned/{active}")]
        public async Task<IActionResult> GetAllVehiclesAssigned(bool active)
        {
            var ans = await _vehicleService.FindAllVehiclesActiveAsync(active);

            return Ok(ans);
        }
       
        [HttpPost]
        public async Task<IActionResult> Post(AddVehicleDto addVehicleDto)
        {
            var ans = await _vehicleService.AddVehicleAsync(addVehicleDto);
            return Ok(ans);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDto updateVehicleDto)
        {
            var ans = await _vehicleService.UpdateVehicleAsync(id, updateVehicleDto);
            return Ok(ans);
        }
       

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ans = await _vehicleService.DeleteVehicleAsync(id);
            return Ok(ans);
        }


    }
}
