using Fleet.Principal.Dtos.PartsDtos.StockOutDtos;

namespace Fleet.Principal.Services.PartsServices.Interfaces
{
    public interface IStockOutService
    {

        public Task<StockOutDto> GetAsync(int id);
        public Task<IEnumerable<StockOutDto>> GetAllAsync();
        public Task<StockOutDto> CreateAsync(AddStockOutDto addStockOutDto);
        public Task<StockOutDto> UpdateAsync(int id,UpdateStockOutDto updateStockOutDto );
        public Task<StockOutDto> DeleteAsync(int id);
  
    }
}
