using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.TransportServices
{
    public class OrgaoService : IOrgaoService
    {
        private readonly OrgaoManager.OrgaoManagerClient _orgaoManagerClient;
        private readonly IMapper _mapper;

        public ObservableCollection<OrgaoDto> orgaos { get;} = new ObservableCollection<OrgaoDto>();
        public OrgaoService(OrgaoManager.OrgaoManagerClient orgaoManagerClient, IMapper mapper) {
            _orgaoManagerClient = orgaoManagerClient;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrgaoDto>> FindAllOrgaos()
        {
            using var list = _orgaoManagerClient.FindAllOrgaos(new FindAllOrgaosRequest());

            if(orgaos.Any())
                orgaos.Clear();

            while (await list.ResponseStream.MoveNext())
            {
                var orgao = list.ResponseStream.Current;
                orgaos.Add(_mapper.Map<OrgaoDto>(orgao));   
            }

            return orgaos;
            
        }

        public async Task<OrgaoDto> FindOrgao(int id)
        {
            var orgao = await _orgaoManagerClient.FindOrgaoAsync(new FindOrgaoRequest { Id = id });

            if(orgao.Id <=0) return new OrgaoDto();
            return _mapper.Map<OrgaoDto>(orgao);
        }
    }
}
