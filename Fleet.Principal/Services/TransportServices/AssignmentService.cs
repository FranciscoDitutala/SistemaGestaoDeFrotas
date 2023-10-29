using AutoMapper;
using Fleet.Principal.Dtos.TransPortDtos.AssignmentDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Grpc.Core;
using System.Collections.ObjectModel;

namespace Fleet.Principal.Services.TransportServices
{
    public class AssignmentService : IAssignmentService
    {

        private readonly AssignmentManager.AssignmentManagerClient _assignmentManagerClient;
        private readonly IMapper _mapper;

        public ObservableCollection<AssignmentDto> Assignments { get; set; }= new ObservableCollection<AssignmentDto>();
        public AssignmentService(AssignmentManager.AssignmentManagerClient assignmentManagerClient, IMapper mapper)
        {
            _assignmentManagerClient = assignmentManagerClient;
            _mapper = mapper;
        }

        public async Task<AssignmentDto> AddAssignmentVehicleAsync(AddAssignmentDto addAssignmentDto)
        {
            var assign = await _assignmentManagerClient.AddAssignmentVehicleAsync(_mapper.Map<AssignmentPayload>(addAssignmentDto));

            return _mapper.Map<AssignmentDto>(assign);
        }

        public async Task<AssignmentDto> FindAssignmentAsync(int id)
        {
            var assign = await _assignmentManagerClient.FindAssignmentAsync( new FindAssignmentRequest { Id = id });

            if (assign.Id <= 0) return new AssignmentDto();

            return _mapper.Map<AssignmentDto>(assign);
        }

        public async Task<IEnumerable<AssignmentDto>> FindAllAssignmentsByVehicleAsync(int vehicleId)
        {
             using var list = _assignmentManagerClient.FindAllAssignmentsByVehicle( new FindAllAssignmentsByVehicleRequest { VehicleId = vehicleId});

            if (Assignments.Any()) 
                Assignments.Clear();
            while(await list.ResponseStream.MoveNext())
            {
                var assign = list.ResponseStream.Current;
                Assignments.Add(_mapper.Map<AssignmentDto>(assign));
            }

            return Assignments;
        }

        public async Task<AssignmentDto> RemoveAssignmentVehicleAsync(int id)
        {
            var assign = await _assignmentManagerClient.RemoveAssignmentVehicleAsync(new FindAssignmentRequest { Id = id });

            if (assign.Id <= 0) return new AssignmentDto();

            return _mapper.Map<AssignmentDto>(assign);
        }

        public async Task<AssignmentDto> UpdateAssignmentVehicleAsync(int id,UpdateAssignmentDto updateAssigmentDto)
        {
            var assign = await _assignmentManagerClient.FindAssignmentAsync(new FindAssignmentRequest { Id = id });
            var ass = _mapper.Map(updateAssigmentDto, assign);
            var assignUpdate = await _assignmentManagerClient.UpdateAssignmentAsync(ass);

            return _mapper.Map<AssignmentDto>(assignUpdate);
        }

        public async Task<AssignmentDto> FindAssignmentVehicleAsync(int vehicleId)
        {
            var assign = await _assignmentManagerClient.FindAssignmentVehicleAsync(new FindAssignmentVehicleRequest { VehicleId = vehicleId });

            if (assign.Id <= 0)
                return new AssignmentDto();

            return _mapper.Map<AssignmentDto>(assign);
        }
    }
}
