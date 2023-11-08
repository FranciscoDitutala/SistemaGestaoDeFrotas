using AutoMapper;
using Fleet.Parts;
using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.PartsServices
{
    public class StockEntryService : IStockEntryService
    {
        private readonly StockEntryManager.StockEntryManagerClient _stockEntryManagerClient;
        private readonly IMapper _mapper;

        public StockEntryService(StockEntryManager.StockEntryManagerClient stockEntryManagerClient, IMapper mapper) {
            _stockEntryManagerClient = stockEntryManagerClient;
            _mapper = mapper;
        }

        public async Task<StockEntryDto> CreateAsync(CreateStockEntryDto createStockEntryDto)
        {
            var entry = await _stockEntryManagerClient.CreateAsync(_mapper.Map<CreateStockEntryRequest>(createStockEntryDto));

            return _mapper.Map<StockEntryDto>(entry);

            
        }
        public async Task<StockEntryDto> DeleteAsync(int id)
        {
            var entry= await _stockEntryManagerClient.DeleteAsync(new DeleteStockEntryRequest { Id = id });
            return _mapper.Map<StockEntryDto>(entry);
        }


        public ObservableCollection<StockEntryDto> stockEntryDtos { get; } = new ();
        public async Task<IEnumerable<StockEntryDto>> FindAllAsync(bool dateRange, DateTime fromDate, DateTime toDate)
        {
            using var list = _stockEntryManagerClient.FindAll(new FindStockEntriesRequest
            {
                ByDateRange = dateRange, 
                FromDate=_mapper.Map<Timestamp>(fromDate),
                ToDate= _mapper.Map<Timestamp>(fromDate)
            });

            if(stockEntryDtos.Any())
                stockEntryDtos.Clear();

            while(await list.ResponseStream.MoveNext())
            {
                var entry = list.ResponseStream.Current;
                stockEntryDtos.Add(_mapper.Map<StockEntryDto>(entry));
            }

            return stockEntryDtos;
        }
        public async Task<StockEntryDto> FindAsync(int id)
        {
            var entry = await _stockEntryManagerClient.findAsync(new FindStockEntryRequest { Id = id });
            return _mapper.Map<StockEntryDto>(entry);
        }

        public async Task<StockEntryDto> UpdateAsync(int id, UpdateStockEntryDto updateStockyEntryDto)
        {
            var entry = await  _stockEntryManagerClient.findAsync(new FindStockEntryRequest { Id = id });
            var update = _mapper.Map(updateStockyEntryDto, entry);

            var updated= await _stockEntryManagerClient.UpdateAsync(_mapper.Map<UpdateStockEntryRequest>(update));

            return _mapper.Map<StockEntryDto>(updated);

        }

        
    }
}
