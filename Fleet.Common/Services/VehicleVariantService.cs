using Fleet.Common.Data;
using Grpc.Core;

namespace Fleet.Common.Services;

public class VehicleVariantService : VehicleVariantManager.VehicleVariantManagerBase
{
    private readonly VehicleModelRepository _vehicleModelRepository;
    private readonly VehicleVariantRepository _vehicleVariantRepository;
    private readonly IMapper _mapper;

    public VehicleVariantService(VehicleModelRepository vehicleModelRepository, VehicleVariantRepository vehicleVariantRepository, IMapper mapper)
    {
        _vehicleModelRepository = vehicleModelRepository;
        _vehicleVariantRepository = vehicleVariantRepository;
        _mapper = mapper;
    }

    public override async Task<VehicleVariantPayload> CreateVariant(CreateVehicleVariantRequest request, ServerCallContext context)
    {
        var vehicleVariant = await _vehicleVariantRepository.Entities.InsertAsync(_mapper.Map<VehicleVariant>(request));
        await _vehicleVariantRepository.SaveAsync();
        return _mapper.Map<VehicleVariantPayload>(vehicleVariant);
    }

    public override async  Task<VehicleVariantPayload> DeleteVehicleVariant(FindVehicleVariantRequest request, ServerCallContext context)
    {
        // My code
        var vehicleVariant = await _vehicleVariantRepository.Entities.FindAsync(x => x.Id == request.Id);

        await _vehicleVariantRepository.Entities.DeleteAsync(request.Id);
        await _vehicleVariantRepository.SaveAsync();

        return _mapper.Map<VehicleVariantPayload>(vehicleVariant);
    }

    public override Task<VehicleVariantPayload> DissableVehicleVariant(FindVehicleVariantRequest request, ServerCallContext context)
    {
        return base.DissableVehicleVariant(request, context);
    }

    public override Task<VehicleVariantPayload> EnableVehicleVariant(FindVehicleVariantRequest request, ServerCallContext context)
    {
        return base.EnableVehicleVariant(request, context);
    }

    public override async Task<VehicleVariantPayload> FindVehicleVariant(FindVehicleVariantRequest request, ServerCallContext context)
    {
        // My code
        var vehicleVariant = await _vehicleVariantRepository.Entities.FindAsync(x => x.Id == request.Id);
        return _mapper.Map<VehicleVariantPayload>(vehicleVariant);
    }

    public override async Task FindVehicleVariantsByModel(FindVehicleVariantsByModelRequest request, IServerStreamWriter<VehicleVariantPayload> responseStream, ServerCallContext context)
    {
        var model = await _vehicleModelRepository.Entities
            .FindAsync(m => m.Id == request.VehicleModelId, new List<string> { nameof(VehicleModel.Variants) });

        if (model is not null)
        {
            foreach (var variant in model.Variants)
            {
                await responseStream.WriteAsync(_mapper.Map<VehicleVariantPayload>(variant));
            }
        }
    }

    public override async Task<VehicleVariantPayload> UpdateVariant(VehicleVariantPayload request, ServerCallContext context)
    {
        var vehicleVariant = _mapper.Map<VehicleVariant>(request);
        await _vehicleVariantRepository.Entities.UpdateAsync(vehicleVariant);

        await _vehicleVariantRepository.SaveAsync();
        return _mapper.Map<VehicleVariantPayload>(vehicleVariant);
    }

    public async Task DeleteAllVehicleVariantById(int id)
    {

        var variantsToDelete = _vehicleVariantRepository.Entities.FilterAsync(x => x.VehicleModelId == id);

        await foreach (var variant in variantsToDelete)
        {
            await _vehicleVariantRepository.Entities.DeleteAsync(variant.Id);
        }

        await _vehicleVariantRepository.SaveAsync();
    }
}

