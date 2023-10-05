using AutoMapper;
using Fleet.Common;
using Fleet.Principal.Dtos.CommonDtos.BrandDtos;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Controllers.CommonControllers
{

    [ApiController]
    [Route("FleetCommon/[controller]")]
    public class VehicleBrandController : ControllerBase
    {


        private readonly IVehicleBrandService _vehicleBrandService;
        private readonly IMapper _mapper;

        public VehicleBrandController(IVehicleBrandService vehicleBrandService, IMapper mapper)
        {
            _vehicleBrandService = vehicleBrandService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var ans = await _vehicleBrandService.GetVehicleBrandAsync(id);
            if (ans == null) return NotFound("A marca Não foi encontrado");
            return Ok(ans);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var ans = await _vehicleBrandService.GetVehicleBrandsAsync();
            return ans.Any() ? Ok(ans) : NotFound("a lista está fazia");
        }

        
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateVehicleBrandDto vehicle)
        {
            var ans = await _vehicleBrandService.CreateVehicleBrandAsync(_mapper.Map<VehicleBrandDto>(vehicle));

            return Ok(ans);
        }
        


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] UpdateVehicleBrandDto vehicle)
        {

            var ans = await _vehicleBrandService.UpdateBrandAsync(id, _mapper.Map<VehicleBrandDto>(vehicle));

            return ans != null ? Ok(" Actualizado com  sucesso") : BadRequest("Marca não Actualizada");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var ans = await _vehicleBrandService.DeleteVehicleBrandAsync(id);

            return  Ok(" Eliminado  com  sucesso") ;
        }

    }
}
