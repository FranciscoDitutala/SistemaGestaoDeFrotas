using AutoMapper;
using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.PartsControllers
{

    
    [ApiController]
    [Route("FleetParts/[controller]")]
    public class StockEntryController : ControllerBase
    {
        private readonly IStockEntryService _stockEntryService;


        public StockEntryController(IStockEntryService stockEntryService) {
            _stockEntryService = stockEntryService;
       
        }

        [HttpGet("GetStockyEntry/{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var ans = await _stockEntryService.FindAsync(id);
            return ans != null?Ok(ans): BadRequest("stockyEntry não encontrado");
           
        }


        [HttpGet("GetStockEntries")]
        public async Task<IActionResult> GetAll()
        {

            var ans = await _stockEntryService.FindAllAsync(false,DateTime.Now,DateTime.Now);
            return ans != null ? Ok(ans) : BadRequest("stockyEntry não encontrado");

        }

        [HttpPost("AddStockEntry")]
        public async Task<IActionResult> Post(CreateStockEntryDto createStockEntryDto)
        {
            var ans = await _stockEntryService.CreateAsync(createStockEntryDto);
            return Ok(ans);
        }

        [HttpPut("UpdateStockEntry/{id}")]
        public async Task<IActionResult> Update(int id, UpdateStockEntryDto updateStockEntryDto)
        {
            var ans = await _stockEntryService.UpdateAsync(id, updateStockEntryDto);
            return Ok(ans);
        }

        /*[HttpDelete("DeleteStockEntry/{id}")]
        public async Task<IActionResult> Delete( int id)
        {
            var ans = await _stockEntryService.DeleteAsync(id); return Ok("Eliminado com sucesso");
        }*/
    }
    
}
