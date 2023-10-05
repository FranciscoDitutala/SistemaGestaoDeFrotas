using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;

namespace Fleet.Principal.Services.PartsServices.Interfaces
{
    public interface IStockEntryService
    {

        public Task<StockEntryDto> CreateAsync(CreateStockEntryDto createStockyEntryDto);
        public Task<IEnumerable<StockEntryDto>> FindAllAsync(bool dateRange, DateTime fromDate, DateTime toDate);
        public Task<StockEntryDto> FindAsync(int id);
        public Task<StockEntryDto> UpdateAsync(int id ,UpdateStockEntryDto updateStockyEntryDto);

        public Task<StockEntryDto> DeleteAsync(int id);
        
    }
}
