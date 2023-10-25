using AutoMapper;
using Fleet.Transport.Data.Entities;
using Fleet.Transport.Data.Repositories;
using Grpc.Core;

namespace Fleet.Transport.Services
{
    public class AssignmentService: AssignmentManager.AssignmentManagerBase
    {
        private readonly AssignmentRepository _assignmentRepository;
        private readonly VehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public AssignmentService(AssignmentRepository assignmentRepository, IMapper mapper, VehicleRepository vehicleRepository)
        {
            _assignmentRepository = assignmentRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public override async Task<AssignmentPayload> AddAssignmentVehicle(AssignmentPayload request, ServerCallContext context)
        {
            var vehicle = await _vehicleRepository.Entities.FindAsync(x => x.Id == request.VehicleId);
            request.Active = true;
            var assignment = await _assignmentRepository.Entities.InsertAsync(_mapper.Map<Assignment>(request));
            if (vehicle != null)
            {

                if (assignment.Type == AssignmentType.Employee)
                {


                    vehicle.EmployeeId = assignment.EmployeeId;
                    vehicle.Assigned = true;

                }else if(request.Type == AssignmentType.Role)
                {
                    vehicle.EmployeeId= assignment.EmployeeId;
                    vehicle.Assigned = true;
                    vehicle.OrgaoId = assignment.OrgaoId;
                }else if (request.Type == AssignmentType.Support)
                {
                    vehicle.OrgaoId= assignment.OrgaoId;
                    vehicle.Assigned = true;
                }

                var v=await _vehicleRepository.Entities.UpdateAsync(vehicle);
            }

           

            await _assignmentRepository.SaveAsync();
            await _vehicleRepository.SaveAsync();
           

            return _mapper.Map<AssignmentPayload>(assignment);
        }

        public override async Task FindAllAssignmentsByVehicle(FindAllAssignmentsByVehicleRequest request, IServerStreamWriter<AssignmentPayload> responseStream, ServerCallContext context)
        {

           await foreach( var assignment in  _assignmentRepository.Entities.FilterAsync(x=> x.VehicleId == request.VehicleId)){
                await responseStream.WriteAsync(_mapper.Map<AssignmentPayload>(assignment));
            }
            
        }
           

        public override async Task<AssignmentPayload> FindAssignment(FindAssignmentRequest request, ServerCallContext context)
        {
            var assignment = await  _assignmentRepository.Entities.FindAsync(x=> x.Id == request.Id);

            return _mapper.Map<AssignmentPayload>(assignment);
        }

        public override async  Task<AssignmentPayload> RemoveAssignmentVehicle(FindAssignmentRequest request, ServerCallContext context)
        {
            var assignment = await _assignmentRepository.Entities.FindAsync(x => x.Id == request.Id);
            if(assignment != null)
            {
                var vehicleId = assignment.VehicleId;
                var vehicle = await _vehicleRepository.Entities.FindAsync(x => x.Id == vehicleId);


                if (vehicle != null)
                {
                    vehicle.EmployeeId = 0;
                    vehicle.Assigned = false;
                    vehicle.OrgaoId = 0;
                    await _vehicleRepository.Entities.UpdateAsync(vehicle);
                }
                assignment.Active = false;
                assignment.EndDateOfAssignment= DateTime.Now;
                assignment = await _assignmentRepository.Entities.UpdateAsync(assignment);
                await _vehicleRepository.SaveAsync();
                await _assignmentRepository.SaveAsync();

            }

            return _mapper.Map<AssignmentPayload>(assignment);

        }
        public override async  Task<AssignmentPayload> UpdateAssignment(AssignmentPayload request, ServerCallContext context)
        {


            var assign = await _assignmentRepository.Entities.UpdateAsync(_mapper.Map<Assignment>(request));
            await _assignmentRepository.SaveAsync();
            return _mapper.Map<AssignmentPayload>(assign);
        }

        public override async Task<AssignmentPayload> FindAssignmentVehicle(FindAssignmentVehicleRequest request, ServerCallContext context)
        {
            var assign = await _assignmentRepository.Entities.FindAsync(x => x.Active == true  && x.VehicleId == request.VehicleId) ?? new Assignment();

            if (assign.Id <= 0) return new AssignmentPayload();

            return _mapper.Map<AssignmentPayload>(assign);
        }

    }
}
