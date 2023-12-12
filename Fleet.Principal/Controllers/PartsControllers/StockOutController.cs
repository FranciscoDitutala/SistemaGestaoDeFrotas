using Fleet.Principal.Dtos.PartsDtos.StockOutDtos;
using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Fleet.Principal.Controllers.PartsControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockOutController : ControllerBase
    {

        private readonly IStockOutService _stockOutService;


        public StockOutController(IStockOutService stockOutService)
        {
            _stockOutService = stockOutService;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var ans = await _stockOutService.GetAsync(id);
            return ans != null ? Ok(ans) : BadRequest("stockyOut não encontrado");

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var ans = await _stockOutService.GetAllAsync();
            return ans != null ? Ok(ans) : BadRequest("stockyOut não encontrado");

        }

        [HttpPost]
        public async Task<IActionResult> Post(AddStockOutDto addStockOutDto )
        {
            var ans = await _stockOutService.CreateAsync(addStockOutDto);
            return Ok(ans);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateStockOutDto updateStockOutDto)
        {
            var ans = await _stockOutService.UpdateAsync(id, updateStockOutDto);
            return Ok(ans);
        }

        [HttpGet("Approve/{id}")]
        public async Task<IActionResult> Approve(int id)
        {

            var entry = await _stockOutService.GetAsync(id);

            if (entry == null)
                return BadRequest("não existe essa entrada");

            StockOutApproveDto stockOutApproveDto = new StockOutApproveDto();

            stockOutApproveDto.Id = entry.Id;

            foreach( var p in entry.RequestedLines )
            {
                stockOutApproveDto.ApprovedLines.Add(p);
            }

            var ans = await _stockOutService.ApproveAsync(stockOutApproveDto);
            return Ok(ans);

        }

        [HttpGet("Cancel/{id}")]
        public async Task<IActionResult> Cancel(int id)
        {

            var ans = await _stockOutService.CancelAsync(id);
            return Ok(ans);

        }
    }
}
