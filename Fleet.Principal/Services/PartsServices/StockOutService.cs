using AutoMapper;
using Fleet.Parts;
using Fleet.Principal.Dtos.PartsDtos.StockOutDtos;
using Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Grpc.Core;
using System.Collections.ObjectModel;
using static Fleet.Parts.StockEntryManager;

namespace Fleet.Principal.Services.PartsServices
{
    public class StockOutService : IStockOutService
    {

        private readonly StockOutManager.StockOutManagerClient _stockOutManagerClient;
        private readonly IMapper _mapper;

        public StockOutService(StockOutManager.StockOutManagerClient stockOutManagerClient, IMapper mapper)
        {
            _stockOutManagerClient = stockOutManagerClient;
            _mapper = mapper;
        }

        public async Task<StockOutDto> CreateAsync(AddStockOutDto addStockOutDto)
        {
            var getOut = await _stockOutManagerClient.CreateAsync(_mapper.Map<CreateStockOutRequest>(addStockOutDto));
            return _mapper.Map<StockOutDto>(getOut);
        }

        public Task<StockOutDto> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<StockOutDto> stockOutDtos { get; } = new();
        public async Task<IEnumerable<StockOutDto>> GetAllAsync()
        {
            using var list = _stockOutManagerClient.FindAll(new FindAllStockOutRequest
            {
                ByDateRange = false
            });

            if (stockOutDtos.Any())
                stockOutDtos.Clear();

            while (await list.ResponseStream.MoveNext())
            {
                var getOut = list.ResponseStream.Current;
                stockOutDtos.Add(_mapper.Map<StockOutDto>(getOut));
            }

            return stockOutDtos;
        }

        public async Task<StockOutDto> GetAsync(int id)
        {
            var getOut = await _stockOutManagerClient.FindAsync(new FindStockOutRequest { Id = id });
            return _mapper.Map<StockOutDto>(getOut);
        }

        public async Task<StockOutDto> UpdateAsync(int id, UpdateStockOutDto updateStockOutDto)
        {
            var entry = await _stockOutManagerClient.FindAsync(new FindStockOutRequest { Id = id });
            var update = _mapper.Map(updateStockOutDto, entry);

            var updated = await _stockOutManagerClient.UpdateAsync(_mapper.Map<UpdateStockOutRequest>(update));

            return _mapper.Map<StockOutDto>(updated);
        }
    }
}
