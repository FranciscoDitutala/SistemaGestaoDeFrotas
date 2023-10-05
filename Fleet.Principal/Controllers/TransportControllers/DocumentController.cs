using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.DocumentDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{
    [ApiController]
    [Route("FleetTransport/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _DocumentService;
        private readonly IMapper _mapper;

        public DocumentController(IDocumentService vehicleDocumentService, IMapper mapper)
        {
            _DocumentService = vehicleDocumentService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentVehicle(int id)
        {
            var ans = await _DocumentService.FindDocumentAsync(id);
            return Ok(ans);
        }
 
        [HttpGet("GetAllDocumentByVehicle/{vehicleId}")]
        public async Task<IActionResult> GetDocumentsByVehicle(int vehicleId)
        {
            var ans = await _DocumentService.FindAllDocumentByVehicleAsync(vehicleId);
            return Ok(ans);
        }
        [HttpGet("GetAllDocumentByAssignment/{assignmentId}")]
        public async Task<IActionResult> GetAllDocumentByAssignment(int assignmentId)
        {
            var ans = await _DocumentService.FindDocumentsByAssignmentsync(assignmentId);
            return Ok(ans);
        }
        [HttpPost]
        public async Task<IActionResult> AddVehicleDocument([FromForm] AddDocumentDto addVehicleDocumentDto)
        {

            var ans = await _DocumentService.AddDocumentAsync(_mapper.Map<DocumentDto>(addVehicleDocumentDto));
            return Ok(ans);
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocumnet(int id, [FromForm] AddDocumentDto addVehicleDocumentDto)
        {
            var ans = await _DocumentService.UpdateDocumentAsync(id, _mapper.Map<UpdateDocumentDto>(addVehicleDocumentDto));
            return Ok(ans);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var ans = await _DocumentService.DeleteDocumentAsync(id);
            return Ok(ans);
        }

    }
}
