using Fleet.Principal.Dtos.CommonDtos.VariantDtos;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.CommonControllers
{

    [ApiController]
    [Route("FleetCommon/[controller]")]
    public class VehicleVariantController : ControllerBase
    {

        IVehicleVariantService _vehicleVariantService;
        public VehicleVariantController(IVehicleVariantService vehicleVariantService)
        {
            _vehicleVariantService = vehicleVariantService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
        
            var ans = await _vehicleVariantService.FindVehicleVariantAsync(id);

            return  ans != null?Ok(ans): BadRequest("Variant não encontrado«a");
        }
        [HttpGet("GetVariantsByModel/{modelId}")]
        public async Task<IActionResult> GetAllVariantsByModel(int modelId)
        {
            var list = await _vehicleVariantService.FindVehicleVariantsByModelAsync(modelId, true);

            return list.Any() ? Ok(list) : BadRequest("A lista está vazia");
        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateVehicleVariantDto createVehicleVariantDto)
        {
            var ans = await _vehicleVariantService.CreateVariantAsync(createVehicleVariantDto);

            return ans != null ?Ok(" Variante Criada com sucesso"): BadRequest(" Variante não cadastrado ");
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, UpdateVehicleVariantDto updateVehicleVariantDto)
        {
            var ans = await _vehicleVariantService.UpdateVariantAsync(id, updateVehicleVariantDto);

            return ans != null ? Ok(ans): BadRequest(" erro ao actualizar a variante");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ans = await _vehicleVariantService.DeleteVehicleVariantAsync(id);

            return Ok("eliminado com sucesso");
        }
    }
}
