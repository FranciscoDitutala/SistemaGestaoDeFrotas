
using Fleet.Principal.Dtos.CommonDtos.ModelDtos;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Fleet.Principal.Controllers.CommonControllers
{

    [ApiController]
    [Route("FleetCommon/[Controller]")]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleModelService _vehicleModelService;

        public VehicleModelController(IVehicleModelService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var ans = await _vehicleModelService.GetVehicleModelAsync(id);

            if (ans == null) return NotFound("o Model Não foi encontrado");

            return Ok(ans);
        }

        [HttpGet("GetModelsByBrand/{brandId}")]
        public async Task<IActionResult> GetVehicleModelsByBrand(int brandId)
        {

            var ans = await _vehicleModelService.GetAllVehicleModelsbyBrandAsync(brandId, true);

            return ans.Any() ? Ok(ans) : NotFound("A lista está fazia");
        }


        [HttpPost]
        public async Task<IActionResult> CreateModel(CreateVehicleModelDto createVehicleModelDto)
        {
            var ans = await _vehicleModelService.CreateVehicleModelAsync(createVehicleModelDto);

            return ans != null ? Ok("Criado com sucesso") : BadRequest("Modelo não cadastrado");
        }
        
       
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateVehicleModelDto updateVehicleModelDto)
        {

            var ans = await _vehicleModelService.UpdateVehicleModelAsync(id, updateVehicleModelDto);

            return ans != null ? Ok("Actualizado com sucesso") : BadRequest("Model não actualizado");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ans = await _vehicleModelService.DeleteVehicleModelAsync(id);
            if (ans == null) return BadRequest("Não eliminado");
            return Ok("eliminado com sucesso");
        }

    }
}
