using AutoMapper;
using Fleet.Transport.Data.Entities;
using Fleet.Transport.Data.Repositories;
using FleetException;
using Grpc.Core;

namespace Fleet.Transport.Services
{
    public class VehicleService : VehicleManager.VehicleManagerBase
    {

        private readonly VehicleRepository _vehicleRepository;
        private readonly DocumentService _vehicleDocumentService;
        private readonly IMapper _mapper;
        public VehiclePayload Nulo { get; set; } = new();   
        public VehicleService(VehicleRepository vehicleRepository, DocumentService vehicleDocumentService, IMapper mapper) {
        
            _vehicleRepository = vehicleRepository;
            _vehicleDocumentService = vehicleDocumentService;
            _mapper = mapper;
        }

        public override async  Task<VehiclePayload> AddVehicle(VehiclePayload request, ServerCallContext context)
        {

                var vehicle = await _vehicleRepository.Entities.InsertAsync(_mapper.Map<Vehicle>(request));
                await _vehicleRepository.SaveAsync();
                return _mapper.Map<VehiclePayload>(vehicle);
        }
         
        public override async Task<VehiclePayload> DeleteVehicle(FindVehicleRequest request, ServerCallContext context) 
        {
              var vehicle = await _vehicleRepository.Entities.FindAsync(x => x.Id == request.Id);

            if (vehicle == null) return Nulo;

                await _vehicleRepository.Entities.DeleteAsync(request.Id);
                await _vehicleDocumentService.DeleteAllDocumentById(request.Id);
                await _vehicleRepository.SaveAsync();
            
            return _mapper.Map<VehiclePayload>(vehicle);

        }

        public override async Task FindAllVehicles(FindAllVehiclesRequest request, IServerStreamWriter<VehiclePayload> responseStream, ServerCallContext context)
        {

            try
            {
                await foreach (var vehicle in _vehicleRepository.Entities.FilterAsync())
                {
                    await responseStream.WriteAsync(_mapper.Map<VehiclePayload>(vehicle));
                }
            } catch (Exception e)
            {
                Console.WriteLine("erro a carregar os veiculos" + e.Message);
            }
        }

        public override async Task<VehiclePayload> FindVehicle(FindVehicleRequest request, ServerCallContext context)
        {

                var vehicle = await _vehicleRepository.Entities.FindAsync(b => b.Id == request.Id);
                if (vehicle == null) return Nulo;
                
                return _mapper.Map<VehiclePayload>(vehicle);
    
        }

        public override async Task<VehiclePayload> UpdateVehicle(VehiclePayload request, ServerCallContext context)
        {
                var vehicle = await _vehicleRepository.Entities.UpdateAsync(_mapper.Map<Vehicle>(request));
                await _vehicleRepository.SaveAsync();
                return _mapper.Map<VehiclePayload>(vehicle);
           
        }

      /*  public override async Task FindAllVehiclesByType(FindByTypeRequest request, IServerStreamWriter<VehiclePayload> responseStream, ServerCallContext context)
        {

            await foreach (var vehicle in _vehicleRepository.Entities.FilterAsync(x => x.Type == request.Type))
            {
                await responseStream.WriteAsync(_mapper.Map<VehiclePayload>(vehicle));
            }
        }
      */
        public override async Task FindAllVehiclesAssigned(FindAllVehiclesAssignedRequest request, IServerStreamWriter<VehiclePayload> responseStream, ServerCallContext context)
        {
            await foreach (var vehicle in _vehicleRepository.Entities.FilterAsync(x => x.Assigned == request.Assigned))
            {
                await responseStream.WriteAsync(_mapper.Map<VehiclePayload>(vehicle));
            }
        }
        public override  async  Task FindVehicles(FindVehiclesRequest request, IServerStreamWriter<VehiclePayload> responseStream, ServerCallContext context)
        {
            await foreach (var vehicle in _vehicleRepository.Entities.FilterAsync(x => x.Registration== request.Filter || 
            x.Brand==request.Filter || x.Cor==request.Filter  || x.Type == request.Filter || x.Vin == request.Filter))
            {
                await responseStream.WriteAsync(_mapper.Map<VehiclePayload>(vehicle));
            }
        }
    }
}
