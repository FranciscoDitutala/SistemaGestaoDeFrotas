using Grpc.Core;
using static Grpc.Core.Metadata;

namespace Fleet.Parts.Services;

public class StockOutService : StockOutManager.StockOutManagerBase
{
    private readonly PartRepository _partRepository;
    private readonly StockOutRepository _stockOutRepository;
    private readonly IMapper _mapper;

    public StockOutService(PartRepository partRepository,
        StockOutRepository stockOutRepository,
        IMapper mapper)
    {
        _partRepository = partRepository;
        _stockOutRepository = stockOutRepository;
        _mapper = mapper;
    }

    public override async Task<StockOutPayload> Create(CreateStockOutRequest request, ServerCallContext context)
    {
        var stockOut = _mapper.Map<StockOut>(request);
        bool valid = await IsStockOutValid(stockOut);
        if (valid)
        {
            stockOut.RegisteredDate = DateTime.UtcNow;
            stockOut.RegisteredBy = "2016492984";
            stockOut.LastUpdated = DateTime.UtcNow;
            stockOut.LastUpdatedBy = "2016492984";

            stockOut = await _stockOutRepository.Entities.InsertAsync(stockOut);

            await _stockOutRepository.SaveAsync();
        }

        return await UpdateLines(_mapper.Map<StockOutPayload>(stockOut));
    }

    public override Task<StockOutPayload> Delete(DeleteStockOutRequest request, ServerCallContext context)
    {
        return base.Delete(request, context);
    }

    public override async Task FindAll(FindAllStockOutRequest request, IServerStreamWriter<StockOutPayload> responseStream, ServerCallContext context)
    {
        await foreach (var stockOut in _stockOutRepository.Entities.FilterAsync(o => !request.ByDateRange ||
        (o.RegisteredDate >= _mapper.Map<DateTime>(request.FromDate) && o.RegisteredDate <= _mapper.Map<DateTime>(request.ToDate))))
        {
            var result = _mapper.Map<StockOutPayload>(stockOut);

            await responseStream.WriteAsync(await UpdateLines(result));
        }
    }

    public override async Task<StockOutPayload> Update(UpdateStockOutRequest request, ServerCallContext context)
    {
        var newStockOut = _mapper.Map<StockOut>(request);
        var currentStockOut = await _stockOutRepository.Entities.FindAsync(o => o.Id == request.Id);
        if (currentStockOut is not null && CanUpdateStockOut(currentStockOut) && await IsStockOutValid(newStockOut))
        {
            //newEntry = _mapper.Map(request, currentEntry);
            newStockOut.Id = 0;
            newStockOut.RequestedLines = new(request.RequestedLines.Select(_mapper.Map<StockOutLine>));
            newStockOut.RegisteredDate = currentStockOut.RegisteredDate;
            newStockOut.RegisteredBy = currentStockOut.RegisteredBy;
            newStockOut.LastUpdated = DateTime.UtcNow;
            newStockOut.LastUpdatedBy = "2016492984";

            await _stockOutRepository.Entities.DeleteAsync(currentStockOut.Id);
            newStockOut = await _stockOutRepository.Entities.InsertAsync(newStockOut);
            await _stockOutRepository.SaveAsync();
        }

        return await UpdateLines(_mapper.Map<StockOutPayload>(newStockOut));
    }

    public override async Task<StockOutPayload> Approve(StockOutApproveRequest request, ServerCallContext context)
    {
        var stockOut = await _stockOutRepository.Entities.FindAsync(o => o.Id == request.Id, new List<string> { nameof(StockOut.ApprovedLines) });
        if(stockOut is not null && await CanApproveStockOut(request))
        {
            stockOut.LastUpdated = DateTime.UtcNow;
            stockOut.LastUpdatedBy = "2016492984";

            stockOut.ApprovedDate = DateTime.UtcNow;
            stockOut.ApprovedBy = "2016492984";

            stockOut.ApprovedLines = new(request.ApprovedLines.Select(_mapper.Map<StockOutLine>));

            await _stockOutRepository.SaveAsync();

            foreach (var line in stockOut.ApprovedLines)
            {
                var part = await _partRepository.Entities.FindAsync(p => p.UPC == line.PartUPC);
                part!.StockQty -= line.Quantity;

                await _partRepository.Entities.UpdateAsync(part);
                await _partRepository.SaveAsync();
            }
        }

        return await UpdateLines(_mapper.Map<StockOutPayload>(stockOut));
    }

    public override async Task<StockOutPayload> Cancel(StockOutCancelRequest request, ServerCallContext context)
    {
        var stockOut = await _stockOutRepository.Entities.FindAsync(o => o.Id == request.Id);
        if (stockOut is not null)
        {
            stockOut.LastUpdated = DateTime.UtcNow;
            stockOut.LastUpdatedBy = "2016492984";

            stockOut.CancelledDate = DateTime.UtcNow;
            stockOut.CancelledBy = "2016492984";

            await _stockOutRepository.SaveAsync();
        }

        return await UpdateLines(_mapper.Map<StockOutPayload>(stockOut));
    }

    public override async Task<StockOutPayload> Deliver(StockOutODeliverRequest request, ServerCallContext context)
    {
        var stockOut = await _stockOutRepository.Entities.FindAsync(o => o.Id == request.Id);
        if (stockOut is not null)
        {
            stockOut.LastUpdated = DateTime.UtcNow;
            stockOut.LastUpdatedBy = "2016492984";

            stockOut.DeliveredDate = DateTime.UtcNow;
            stockOut.DeliveredBy = "2016492984";
            stockOut.DeliveredTo = request.DeliveredTo;

            await _stockOutRepository.SaveAsync();
        }

        return await UpdateLines(_mapper.Map<StockOutPayload>(stockOut));
    }

    private async Task<bool> IsStockOutValid(StockOut stockOut)
    {
        var valid = stockOut.RequestedLines.Any();
        foreach (var line in stockOut.RequestedLines)
        {
            if (!await _partRepository.Entities.AnyAsync(p => p.UPC == line.PartUPC))
                valid = false;
        }

        return valid;
    }

    private static bool CanUpdateStockOut(StockOut stockOut)
    {
        return string.IsNullOrWhiteSpace(stockOut.ApprovedBy)
            && string.IsNullOrWhiteSpace(stockOut.CancelledBy);
    }

    private async Task<bool> CanApproveStockOut(StockOutApproveRequest request)
    {
        if(!request.ApprovedLines.Any())
            return false;

        foreach (var line in request.ApprovedLines)
        {
            var part = await _partRepository.Entities.FindAsync(p => p.UPC == line.PartUPC);
            if (part is null || part.StockQty - line.Quantity < 0.0m)
                return false;
        }

        return true;
    }

    private async Task<StockOutPayload> UpdateLines(StockOutPayload result)
    {
        foreach (var linePayload in result.RequestedLines)
        {
            var part = await _partRepository.Entities.FindAsync(p => p.UPC == linePayload.PartUPC);
            linePayload.PartTypeName = part?.PartTypeName ?? string.Empty;
            linePayload.Description = part?.Description ?? string.Empty;
            linePayload.StockQty = part?.StockQty ?? 0.0m;
        }

        if (result.Approved)
        {
            foreach (var linePayload in result.ApprovedLines)
            {
                var part = await _partRepository.Entities.FindAsync(p => p.UPC == linePayload.PartUPC);
                linePayload.PartTypeName = part?.PartTypeName ?? string.Empty;
                linePayload.Description = part?.Description ?? string.Empty;
                linePayload.StockQty = part?.StockQty ?? 0.0m;
            }
        }

        return result;
    }
}
