using Fleet.Principal.Dtos.TransPortDtos.AssignmentDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{
    [ApiController]
    [Route("FleetTransport/[controller]")]
    public class AssignmentController : ControllerBase
    {

        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ans =  await _assignmentService.FindAssignmentAsync(id);

            if(ans.Id <=0) return NotFound(" atribuição não encontarda");
            return Ok(ans);
        }

        [HttpGet("GetAllAssignmentsByVehicle/{vehicleId}")]
        public async Task<IActionResult> GetAllAssignmentsByVehicle(int vehicleId )
        {
            var ans = await _assignmentService.FindAllAssignmentsByVehicleAsync(vehicleId);
            return Ok(ans);
        }
        [HttpGet("GetAssignmentVehicle/{vehicleId}")]
        public async Task<IActionResult> GetAssignmentVehicle(int vehicleId)
        {
            var ans = await _assignmentService.FindAssignmentVehicleAsync(vehicleId);

            if (ans.Id <= 0) return NotFound("O veiculo não esta atribuido");
            return Ok(ans);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddAssignmentDto addAssignmentDto)
        {
            var ans = await _assignmentService.AddAssignmentVehicleAsync(addAssignmentDto);
            return Ok(ans);
        }
        [HttpPut]
        public async Task<IActionResult> Put(int id, UpdateAssignmentDto updateAssignmentDto)
        {
            var ans = await _assignmentService.UpdateAssignmentVehicleAsync(id, updateAssignmentDto);
            return Ok(ans);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ans = await _assignmentService.RemoveAssignmentVehicleAsync(id);

            if (ans.Id <= 0) return NotFound("O veiculo não esta atribuido");
            return Ok(ans);
        }
    }
}
