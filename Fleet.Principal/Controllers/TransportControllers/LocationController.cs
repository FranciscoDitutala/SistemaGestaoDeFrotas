using AutoMapper;
using Fleet.Principal.Services.TransportServices;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.TransportControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationservice;
        private readonly IMapper _mapper;

        public LocationController(ILocationService locationservice, IMapper mapper)
        {
            _locationservice = locationservice;
            _mapper = mapper;
        }
        [HttpGet("GetLocationById/{id:int}")]
        public async Task<IActionResult> GetLocationById(int id) { 

            var result= await _locationservice.GetLocationById(id);
            return Ok(result);
        }

        [HttpGet("GetLocationByRegistration/{registration}")]
        public async Task<IActionResult> GetLocationByRegistration(string registration)
        {

            var result = await _locationservice.GetLocationByRegistration(registration);

            return Ok(result);

        }

    }
}
