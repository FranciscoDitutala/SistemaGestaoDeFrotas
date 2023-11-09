using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.DocumentDtos;
using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Principal.Services.TransportServices;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{
    [Authorize]
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

       
        /*
        [HttpGet("GetAllVehiclesbyType/{type}")]
        public async Task<IActionResult> GetAllVehiclesByType(string type)
        {
            var ans = await _vehicleService.FindAllVehiclesByTypeAsync( type);

            return Ok(ans);
        }
        */
        [HttpGet("GetVehicles/{filter}")]
        public async Task<IActionResult> GetVehicles(string filter)
        {
            var ans = await _vehicleService.FindVehiclesAsync(filter);
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

            try { 
                    var ver = await _vehicleService.VerificarVinReg(addVehicleDto);
                    if (ver == false) return BadRequest("A matricula ou Vin do veículo já existe");

                        var ans = await _vehicleService.AddVehicleAsync(addVehicleDto);
                        return Ok(ans);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

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
