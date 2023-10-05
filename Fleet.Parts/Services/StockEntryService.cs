using Grpc.Core;

namespace Fleet.Parts.Services;

public class StockEntryService : StockEntryManager.StockEntryManagerBase
{
    private readonly PartRepository _partRepository;
    private readonly StockEntryRepository _stockEntryRepository;
    private readonly IMapper _mapper;

    public StockEntryService(
        PartRepository partRepository,
        StockEntryRepository stockEntryRepository, IMapper mapper)
    {
        _partRepository = partRepository;
        _stockEntryRepository = stockEntryRepository;
        _mapper = mapper;
    }

    public override async Task<StockEntryPayload> Create(CreateStockEntryRequest request, ServerCallContext context)
    {
        var entry = _mapper.Map<StockEntry>(request);
        bool valid = await IsEntryValid(entry);
        if (valid)
        {
            entry.RegisteredDate = DateTime.UtcNow;
            entry.RegisteredBy = "2016492984";
            entry.LastUpdated = DateTime.UtcNow;
            entry.LastUpdatedBy = "2016492984";

            entry = await _stockEntryRepository.Entities.InsertAsync(entry);

            await _stockEntryRepository.SaveAsync();

            foreach (var line in entry.Lines)
            {
                var part = await _partRepository.Entities.FindAsync(p => p.UPC == line.PartUPC);
                part!.StockQty += line.Quantity;

                await _partRepository.Entities.UpdateAsync(part);
                await _partRepository.SaveAsync();
            }
        }

        return await UpdateLines(_mapper.Map<StockEntryPayload>(entry));
    }


    // My code
    public override async Task<StockEntryPayload> find(FindStockEntryRequest request, ServerCallContext context)
    {
        var entry = await  _stockEntryRepository.Entities.FindAsync(x => x.Id == request.Id);

        return _mapper.Map<StockEntryPayload>(entry);
    }

    // My code
    public override async Task<StockEntryPayload> Delete(DeleteStockEntryRequest request, ServerCallContext context)
    {
        var entry = await _stockEntryRepository.Entities.FindAsync(x=> x.Id == request.Id);

        if(entry != null)
        {
           await  _stockEntryRepository.Entities.DeleteAsync(request.Id);
           await _stockEntryRepository.SaveAsync();
        }
        return _mapper.Map<StockEntryPayload>(entry);
    }

    public override async Task FindAll(FindStockEntriesRequest request, IServerStreamWriter<StockEntryPayload> responseStream, ServerCallContext context)
    {
        await foreach (var entry in _stockEntryRepository.Entities.FilterAsync(e => !request.ByDateRange ||
        (e.RegisteredDate >= _mapper.Map<DateTime>(request.FromDate) && e.RegisteredDate <= _mapper.Map<DateTime>(request.ToDate))))
        {
            var result = _mapper.Map<StockEntryPayload>(entry);

            await responseStream.WriteAsync(await UpdateLines(result));
        }
    }

    public override async Task<StockEntryPayload> Update(UpdateStockEntryRequest request, ServerCallContext context)
    {
        var newEntry = _mapper.Map<StockEntry>(request);
        var currentEntry = await _stockEntryRepository.Entities.FindAsync(e => e.Id == request.Id, new List<string> { nameof(StockEntry.Lines) });
        if(currentEntry is not null && await CanUpdateEntry(currentEntry) && await IsEntryValid(newEntry))
        {
            foreach (var line in currentEntry.Lines)
            {
                var part = await _partRepository.Entities.FindAsync(p => p.UPC == line.PartUPC);
                part!.StockQty -= line.Quantity;

                //var newLine = newEntry.Lines.FirstOrDefault(l => l.PartUPC == part.UPC);
                //if(newLine is not null)
                //    part.StockQty += newLine.Quantity;

                await _partRepository.Entities.UpdateAsync(part);
                //await _partRepository.SaveAsync();
            }

            foreach (var line in newEntry.Lines)
            {
                var part = await _partRepository.Entities.FindAsync(p => p.UPC == line.PartUPC);
                part!.StockQty += line.Quantity;

                await _partRepository.Entities.UpdateAsync(part);
                //await _partRepository.SaveAsync();
            }

            await _partRepository.SaveAsync();

            newEntry = _mapper.Map(request, currentEntry);
            //newEntry.Id = 0;
            //newEntry.Lines = new(request.Lines.Select(_mapper.Map<StockEntryLine>));
            //newEntry.RegisteredDate = currentEntry.RegisteredDate;
            //newEntry.RegisteredBy = currentEntry.RegisteredBy;
            newEntry.LastUpdated = DateTime.UtcNow;
            newEntry.LastUpdatedBy = "2016492984";

            //await _stockEntryRepository.Entities.DeleteAsync(currentEntry.Id);
            //newEntry = await _stockEntryRepository.Entities.InsertAsync(newEntry);
            await _stockEntryRepository.SaveAsync();
            return await UpdateLines(_mapper.Map<StockEntryPayload>(newEntry));
        }
        return await UpdateLines(_mapper.Map<StockEntryPayload>(currentEntry));
    }

    private async Task<bool> IsEntryValid(StockEntry entry)
    {
        var valid = entry.Lines.Any();
        foreach (var line in entry.Lines)
        {
            if (!await _partRepository.Entities.AnyAsync(p => p.UPC == line.PartUPC))
                valid = false;
        }

        return valid;
    }

    private async Task<bool> CanUpdateEntry(StockEntry entry)
    {
        foreach (var line in entry.Lines)
        {
            var part = await _partRepository.Entities.FindAsync(p => p.UPC == line.PartUPC);
            if (part is null || part.StockQty - line.Quantity < 0.0m)
                return false;
        }

        return true;
    }

    private async Task<StockEntryPayload> UpdateLines(StockEntryPayload result)
    {
        foreach (var linePayload in result.Lines)
        {
            var part = await _partRepository.Entities.FindAsync(p => p.UPC == linePayload.PartUPC);
            linePayload.PartTypeName = part?.PartTypeName ?? string.Empty;
            linePayload.Description = part?.Description ?? string.Empty;
        }

        return result;
    }
}
