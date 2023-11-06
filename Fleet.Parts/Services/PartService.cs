using Google.Protobuf;
using Grpc.Core;

namespace Fleet.Parts.Services;

public class PartService : PartManager.PartManagerBase
{
    private readonly PartRepository _partRepository;
    private readonly PartCategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public PartService(PartCategoryRepository categoryRepository, PartRepository partRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _partRepository = partRepository;
        _mapper = mapper;
    }

    public override async Task<PartPayload> Create(CreatePartRequest request, ServerCallContext context)
    {
        var part = await _partRepository.Entities.InsertAsync(_mapper.Map<Part>(request));
        await _partRepository.SaveAsync();
        return _mapper.Map<PartPayload>(part);
    }

    public override Task<PartPayload> Delete(DeletePartRequest request, ServerCallContext context)
    {
        return base.Delete(request, context);
    }

    public override async Task<PartPayload> Find(FindPartRequest request, ServerCallContext context)
    {
        var part = await _partRepository.Entities.FindAsync(p => p.UPC == request.Code || p.SKU == request.Code) ?? new Part();
        return _mapper.Map<PartPayload>(part);
    }

    public override async Task FindAllByType(FindPartsByTypeRequest request, IServerStreamWriter<PartPayload> responseStream, ServerCallContext context)
    {
        await foreach (var part in _partRepository.Entities.FilterAsync( p => p.PartTypeName == request.PartTypeName))
        {
            await responseStream.WriteAsync(_mapper.Map<PartPayload>(part));
        }
    }

    public override async Task FindCategories(FindCategoriesRequest request, IServerStreamWriter<PartCategoryPayload> responseStream, ServerCallContext context)
    {
        await foreach (var category in _categoryRepository.Entities.FilterAsync())
        {
            await responseStream.WriteAsync(_mapper.Map<PartCategoryPayload>(category));
        }
    }

    public override async Task FindTypesByCategory(FindTypesByCategoryRequest request, IServerStreamWriter<PartTypePayload> responseStream, ServerCallContext context)
    {
        var category = await _categoryRepository.Entities.FindAsync(c => c.Name == request.PartCategoryName,
            new List<string>
            { 
                nameof(PartCategory.PartTypeCategories),
                $"{nameof(PartCategory.PartTypeCategories)}.{nameof(PartTypeCategory.PartType)}"
            });

        if (category is not null)
        {
            foreach (var typeCategory in category.PartTypeCategories)
            {
                var type = typeCategory.PartType;
                await responseStream.WriteAsync(new()
                {
                    Name = typeCategory.PartTypeName,
                    SubCategory = typeCategory.SubCategory,
                    Image = _mapper.Map<ByteString>(type.Image)
                });
            } 
        }
    }

    public override async Task<PartPayload> Update(PartPayload request, ServerCallContext context)
    {
        var part = _mapper.Map<Part>(request);
        await _partRepository.Entities.UpdateAsync(part);

        await _partRepository.SaveAsync();
        return _mapper.Map<PartPayload>(part);
    }

    public override async  Task FindAllPart(FindPartsRequest request, IServerStreamWriter<PartPayload> responseStream, ServerCallContext context)
    {
        await foreach (var part in _partRepository.Entities.FilterAsync())
        {
            await responseStream.WriteAsync(_mapper.Map<PartPayload>(part));
        }
    }
}
