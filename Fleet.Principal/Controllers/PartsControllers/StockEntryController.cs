using AutoMapper;
using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;
using Fleet.Principal.Model;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.ObjectModel;

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

        private ObservableCollection<StockEntryDto> list = new ();
        private ObservableCollection<StockEntryDto> list2 = new();
        [HttpGet("GetStockyEntry/{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var ans = await _stockEntryService.FindAsync(id);
            return ans != null?Ok(ans): BadRequest("stockyEntry não encontrado");
           
        }


        [HttpGet("GetStockEntries")]
        public async Task<IActionResult> GetAll()
        {
          

            if (list.Any())
            {
                list2.Clear();
                foreach(var x in list)
                {
                    list2.Add(x);
                }
                list.Clear();

                return Ok(list2);
            }
                
            var ans = await _stockEntryService.FindAllAsync(false,DateTime.Now,DateTime.Now);
            return ans != null ? Ok(ans) : BadRequest("stockyEntry não encontrado");

        }

        [HttpPost("GetStockEntries")]
        public async Task<IActionResult> GetStockByDate(DateModel dateModel)
        {

            if(list.Any())
               list.Clear();
            var ans = await _stockEntryService.FindAllAsync(true, dateModel.FromDate,dateModel.ToDate);

            foreach ( var x in ans)
            {
                list.Add(x);
            }
            return Redirect("GetStockEntries");

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
