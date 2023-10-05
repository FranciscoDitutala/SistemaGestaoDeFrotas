using Grpc.Core;
using System.Collections;

namespace Fleet.Common.Services;

public class VehicleModelService : VehicleModelManager.VehicleModelManagerBase
{
    private readonly VehicleBrandRepository _vehicleBrandRepository;
    private readonly VehicleModelRepository _vehicleModelRepository;
    private readonly VehicleVariantService _vehicleVariantService;
    private readonly IMapper _mapper;

    public VehicleModelService(VehicleBrandRepository vehicleBrandRepository, VehicleModelRepository vehicleModelRepository,
        VehicleVariantService vehicleVariantService,IMapper mapper)
    {
        _vehicleBrandRepository = vehicleBrandRepository;
        _vehicleModelRepository = vehicleModelRepository;
        _vehicleVariantService = vehicleVariantService;
        _mapper = mapper;
    }

    public override async Task<VehicleModelPayload> CreateModel(CreateVehicleModelRequest request, ServerCallContext context)
    {
        var vehicleModel = await _vehicleModelRepository.Entities.InsertAsync(_mapper.Map<VehicleModel>(request));
        await _vehicleModelRepository.SaveAsync();
        return _mapper.Map<VehicleModelPayload>(vehicleModel);
    }

    public override async  Task<VehicleModelPayload> DeleteVehicleModel(FindVehicleModelRequest request, ServerCallContext context)
    {

        // Gylmar code
        int id= request.Id;
        await _vehicleModelRepository.Entities.DeleteAsync(id);
        await _vehicleVariantService.DeleteAllVehicleVariantById(id);
        await  _vehicleModelRepository.SaveAsync();
        return new VehicleModelPayload();
    }
    public override Task<VehicleModelPayload> DissableVehicleModel(FindVehicleModelRequest request, ServerCallContext context)
    {
        return base.DissableVehicleModel(request, context);
    }

    public override Task<VehicleModelPayload> EnableVehicleModel(FindVehicleModelRequest request, ServerCallContext context)
    {
        return base.EnableVehicleModel(request, context);
    }

    public override async  Task<VehicleModelPayload> FindVehicleModel(FindVehicleModelRequest request, ServerCallContext context)
    {
        var vehicleModel = await _vehicleModelRepository.Entities.FindAsync(x => x.Id == request.Id);
        return _mapper.Map<VehicleModelPayload>(vehicleModel);
    }

    public override async Task FindVehicleModelsByBrand(FindVehicleModelsByBrandRequest request, IServerStreamWriter<VehicleModelPayload> responseStream, ServerCallContext context)
    {
        var brand = await _vehicleBrandRepository.Entities
            .FindAsync(b => b.Id == request.VehicleBrandId, new List<string> { nameof(VehicleBrand.Models) });

        if (brand is not null)
        {
            foreach (var model in brand.Models)
            {
                await responseStream.WriteAsync(_mapper.Map<VehicleModelPayload>(model));
            }
        }
    }

    public override async Task<VehicleModelPayload> UpdateModel(VehicleModelPayload request, ServerCallContext context)
    {
        var vehicleModel = _mapper.Map<VehicleModel>(request);
        await _vehicleModelRepository.Entities.UpdateAsync(vehicleModel);

        await _vehicleModelRepository.SaveAsync();
        return _mapper.Map<VehicleModelPayload>(vehicleModel);
    }

    public  async Task DeleteAllVehicleModelById(int id)
    {

        var modelsToDelete =  _vehicleModelRepository.Entities.FilterAsync(model => model.VehicleBrandId == id);

        await foreach (var model in modelsToDelete)
        {
            await _vehicleVariantService.DeleteAllVehicleVariantById(model.Id);
            await _vehicleModelRepository.Entities.DeleteAsync(model.Id);
        }

        await _vehicleModelRepository.SaveAsync();
    }
}

