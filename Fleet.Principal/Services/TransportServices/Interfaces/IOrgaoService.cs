using Fleet.Principal.Dtos.TransPortDtos;

namespace Fleet.Principal.Services.TransportServices.Interfaces
{
    public interface IOrgaoService
    {
        public Task<IEnumerable<OrgaoDto>> FindAllOrgaos();
        public Task<OrgaoDto> FindOrgao(int id);
    }
}
