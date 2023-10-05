using AutoMapper;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{

    [ApiController]
    [Route("FleetTransport/[controller]")]
    public class OrgaoController : ControllerBase
    {
   
        private readonly IOrgaoService _orgaoService;
        private readonly IMapper _mapper;

        public OrgaoController(IVehicleService vehicleService,
            IOrgaoService orgaoService, IEmployeeService employeeService, IMapper mapper)
        {
            _orgaoService = orgaoService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrgao(int id)
        {
            var ans = await _orgaoService.FindOrgao(id);
            return Ok(ans);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrgaos()
        {
            var ans = await _orgaoService.FindAllOrgaos();
            return Ok(ans);
        }
    }
}
