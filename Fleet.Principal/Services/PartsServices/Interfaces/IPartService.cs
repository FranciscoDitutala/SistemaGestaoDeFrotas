using Fleet.Parts;
using Fleet.Principal.Dtos.PartsDtos.PartDtos;

namespace Fleet.Principal.Services.PartsServices.Interfaces
{
    public interface IPartService
    {

        public Task<PartDto> CreateAsync(AddPartDto addPartDto);
        public Task<PartDto> FindAsync(string code);
        public Task<IEnumerable<PartDto>> FindAllByTypeAsync(string partTypename, string filter);


        public Task<PartDto> UpdateAsync(string upc, UpdatePartDto updatePartDto);
        public Task<PartDto> DeleteAsync(string upc);

        public Task<IEnumerable<PartCategoryDto>> FindCategoriesAsync(string filter);

        public Task<IEnumerable<PartTypeDto>> FindTypesByCategoryAsync(string partyCategoryName, string filter);
    }
}
