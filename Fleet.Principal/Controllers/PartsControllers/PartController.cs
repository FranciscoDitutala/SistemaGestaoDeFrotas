using Fleet.Principal.Dtos.PartsDtos.PartDtos;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Principal.Controllers.PartsControllers
{


    [ApiController]
    [Route("FleetParts/[controller]")]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;
        public PartController(IPartService partService) {
            _partService = partService;
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetPart(string code)
        {
            var ans = await _partService.FindAsync(code);
            return ans != null ? Ok(ans) : BadRequest("peça não encontrada");
        }

        [HttpGet("GetPartsByType/{partTypename}")]
        public async Task<IActionResult> GetAllByType(string partTypename)
        {
            var ans = await _partService.FindAllByTypeAsync(partTypename, "");

            return Ok(ans);

        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories() {
            var ans = await _partService.FindCategoriesAsync("");
            return Ok(ans);
        }

        [HttpGet("GetTypesByCategory/{partyCategoryName}")]
        public async Task<IActionResult> GetTypesByCategory(string partyCategoryName)
        {
            var ans = await _partService.FindTypesByCategoryAsync(partyCategoryName, "");
            return Ok(ans);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddPartDto addPartDto)
        {
            var ans = await _partService.CreateAsync(addPartDto);
            return Ok(ans);
        }

        [HttpPut("{upc}")]
        public async Task<IActionResult> Put(string upc,UpdatePartDto updatePartDto)
        {
            var ans =await _partService.UpdateAsync(upc, updatePartDto);

            return Ok(ans);
        }
    }
    
}
