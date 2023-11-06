using AutoMapper;
using Fleet.Parts;
using Fleet.Principal.Dtos.PartsDtos.PartDtos;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.PartsServices
{
    public class PartService : IPartService
    {
        private readonly PartManager.PartManagerClient _partManagerClient;
        private readonly IMapper _mapper;

        public PartService(PartManager.PartManagerClient partManagerClient, IMapper mapper) {
            _partManagerClient = partManagerClient;
            _mapper = mapper;
        }

        public async Task<PartDto> CreateAsync(AddPartDto addPartDto)
        {
            var part = _mapper.Map<CreatePartRequest>(addPartDto);
            var ans= await _partManagerClient.CreateAsync(part);
            return _mapper.Map<PartDto>(ans);
        }

        public ObservableCollection<PartCategoryDto> Categories { get; } = new();
        public async Task<IEnumerable<PartCategoryDto>> FindCategoriesAsync(string filter)
        {
            using var list = _partManagerClient.FindCategories( new FindCategoriesRequest { Filter = filter });

            if(Categories.Any())
                Categories.Clear();
            while(await list.ResponseStream.MoveNext())
            {
                var part=list.ResponseStream.Current;
                Categories.Add(_mapper.Map<PartCategoryDto>(part));
            }

            return Categories;
        }

        public async Task<PartDto> FindAsync(string code)
        {
            var part=await _partManagerClient.FindAsync(new FindPartRequest { Code = code });

            return _mapper.Map<PartDto>(part);
        }


        public ObservableCollection<PartDto> PartsbyType { get; } = new();
        public async Task<IEnumerable<PartDto>> FindAllByTypeAsync(string partTypename, string filter)
        {
            using var list = _partManagerClient.FindAllByType(new FindPartsByTypeRequest{ PartTypeName = partTypename, Filter=filter});

            if (PartsbyType.Any())
                PartsbyType.Clear();
            while (await list.ResponseStream.MoveNext())
            {
                var part = list.ResponseStream.Current;
                 PartsbyType.Add(_mapper.Map<PartDto>(part));
            }

            return PartsbyType;
        }

        public ObservableCollection<PartTypeDto> TypesByCategory { get; } = new();
        public async Task<IEnumerable<PartTypeDto>> FindTypesByCategoryAsync(string partyCategoryName, string filter)
        {
            using var list = _partManagerClient.FindTypesByCategory(new FindTypesByCategoryRequest{ PartCategoryName= partyCategoryName, Filter=filter });

            if (TypesByCategory.Any())
                TypesByCategory.Clear();
            while (await list.ResponseStream.MoveNext())
            {
                var part = list.ResponseStream.Current;
                TypesByCategory.Add(_mapper.Map<PartTypeDto>(part));
            }
            return TypesByCategory;
        }

        public Task<PartDto> DeleteAsync(string upc)
        {
            throw new NotImplementedException();
        }

        public Task<PartDto> UpdateAsync(string upc, UpdatePartDto updatePartDto)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<PartDto> Parts { get; } = new();
        public  async Task<IEnumerable<PartDto>> FindAllPartAsync()
        {
            using var list = _partManagerClient.FindAllPart(new FindPartsRequest ());

            if (Parts.Any())
                Parts.Clear();
            while (await list.ResponseStream.MoveNext())
            {
                var part = list.ResponseStream.Current;
                Parts.Add(_mapper.Map<PartDto>(part));
            }
            return Parts;
        }
    }
}
