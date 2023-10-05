using Grpc.Core;

namespace Fleet.Common.Services;

public class VehicleBrandService : VehicleBrandManager.VehicleBrandManagerBase
{
    private readonly VehicleBrandRepository _vehicleBrandRepository;
    private readonly VehicleModelService _vehicleModelService;
    private readonly IMapper _mapper;

    public VehicleBrandService(VehicleBrandRepository vehicleBrandRepository, VehicleModelService vehicleModelService, IMapper mapper)
    {
        _vehicleBrandRepository = vehicleBrandRepository;
        _vehicleModelService = vehicleModelService;
        _mapper = mapper;
    }

    public override async Task<VehicleBrandPayload> CreateBrand(CreateVehicleBrandRequest request, ServerCallContext context)
    {
        var vehicleBrand = await _vehicleBrandRepository.Entities.InsertAsync(_mapper.Map<VehicleBrand>(request));
        await _vehicleBrandRepository.SaveAsync();
        return _mapper.Map<VehicleBrandPayload>(vehicleBrand);
    }

    public override  async Task<VehicleBrandPayload> DeleteVehicleBrand(FindVehicleBrandRequest request, ServerCallContext context)
    {
   
         await  _vehicleBrandRepository.Entities.DeleteAsync(request.Id);
         await   _vehicleModelService.DeleteAllVehicleModelById(request.Id);
          await  _vehicleBrandRepository.SaveAsync();
         return new VehicleBrandPayload();
         
    }

    public override Task<VehicleBrandPayload> DissableVehicleBrand(FindVehicleBrandRequest request, ServerCallContext context)
    {
        return base.DissableVehicleBrand(request, context);
    }

    public override Task<VehicleBrandPayload> EnableVehicleBrand(FindVehicleBrandRequest request, ServerCallContext context)
    {
        return base.EnableVehicleBrand(request, context);
    }

    public override async Task FindAllVehicleBrands(FindAllVehicleBrandsRequest request, IServerStreamWriter<VehicleBrandPayload> responseStream, ServerCallContext context)
    {

        await foreach (var brand in _vehicleBrandRepository.Entities.FilterAsync(request.OnlyEnabled ?
                b => b.Enabled : null))
        {
            await responseStream.WriteAsync(_mapper.Map<VehicleBrandPayload>(brand));
        }
    }

    public override async Task<VehicleBrandPayload> FindVehicleBrand(FindVehicleBrandRequest request, ServerCallContext context)
    {
        var vehicleBrand = await _vehicleBrandRepository.Entities.FindAsync(b => b.Id == request.Id);
        return _mapper.Map<VehicleBrandPayload>(vehicleBrand);
    }

    public override async Task<VehicleBrandPayload> UpdateBrand(VehicleBrandPayload request, ServerCallContext context)
    {
        var vehicleBrand = _mapper.Map<VehicleBrand>(request);
        await _vehicleBrandRepository.Entities.UpdateAsync(vehicleBrand);


        await _vehicleBrandRepository.SaveAsync();
        return _mapper.Map<VehicleBrandPayload>(vehicleBrand);
    }
}

