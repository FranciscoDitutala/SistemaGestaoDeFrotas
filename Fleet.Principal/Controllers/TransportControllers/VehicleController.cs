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
        private readonly VehicleDetailService _vehicleDetailService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, VehicleDetailService vehicleDetailService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _vehicleDetailService = vehicleDetailService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {

            var ans = await _vehicleService.FindVehicleAsync(id);
            if (ans.Id <= 0) return NotFound("O veiculo não foi encontrado");
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
       

        [HttpGet("GetAllVehiclesAssigned/{active}")]
        public async Task<IActionResult> GetAllVehiclesAssigned(bool active)
        {
            var ans = await _vehicleService.FindAllVehiclesActiveAsync(active);

            return Ok(ans);
        }
        [HttpGet("GetVehicleDetail/{id}")]
        public async Task<IActionResult> GetVehicleDetail(int id)
        {

            var ans = await _vehicleDetailService.FindVehicleDetail(id);
            return Ok(ans);

        }


        [HttpPost]
        public async Task<IActionResult> Post(AddVehicleDto addVehicleDto)
        {
            if( addVehicleDto == null)
            {
                return BadRequest(" O veículo não pode ser nullo, Prencher os campos obrigatorio");
            }

            var ans = await _vehicleService.AddVehicleAsync(addVehicleDto);
            return Ok(ans);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, UpdateVehicleDto updateVehicleDto)
        {
            if (updateVehicleDto == null)
            {
                return BadRequest(" O veículo não pode ser nullo, Prencher os campos obrigatorio");
            }
            var ans = await _vehicleService.UpdateVehicleAsync(id, updateVehicleDto);
            return Ok(ans);
        }
       

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ans = await _vehicleService.DeleteVehicleAsync(id);

            if (ans.Id <= 0) return BadRequest("O veiculo não foi encontrado ");
            return Ok(ans);
        }


    }
}
