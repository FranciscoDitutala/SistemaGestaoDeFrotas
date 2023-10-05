using AutoMapper;
using Fleet.Transport.Data.Repositories;
using Grpc.Core;

namespace Fleet.Transport.Services
{
    public class OrgaoService : OrgaoManager.OrgaoManagerBase
    {
        
        private readonly OrgaoRepository _repository;
        private readonly IMapper _mapper;
        public OrgaoService(OrgaoRepository orgaoRepository, IMapper mapper) { 
        
            _repository = orgaoRepository;
            _mapper = mapper;

        }
        public override async Task FindAllOrgaos(FindAllOrgaosRequest request, IServerStreamWriter<OrgaoPayload> responseStream, ServerCallContext context)
        {
             foreach (var orgao in _repository.Orgaos)
            {
                await responseStream.WriteAsync(_mapper.Map<OrgaoPayload>(orgao));
            }
        }

        public override async Task<OrgaoPayload> FindOrgao(FindOrgaoRequest request, ServerCallContext context)
        {
            var orgao= _repository.Find(request.Id);

            return _mapper.Map<OrgaoPayload>(orgao);
        }
    }
}
