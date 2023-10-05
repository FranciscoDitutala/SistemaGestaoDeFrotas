using AutoMapper;
using Microsoft.Maui.ApplicationModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

public partial class VehicleBrandsViewModel : BaseViewModel<VehicleBrandsViewModel>, IRecipient<VehicleBrandChangedMessage>
{
    readonly IConnectivity _connectivity;
    readonly IMessenger _messenger;
    readonly VehicleBrandManager.VehicleBrandManagerClient _brandService;
    readonly IMapper _mapper;

    public VehicleBrandsViewModel(VehicleBrandManager.VehicleBrandManagerClient brandService, IConnectivity connectivity, IMessenger messenger, IMapper mapper)
    {
        _brandService = brandService;
        _connectivity = connectivity;
        _messenger = messenger;
        _mapper = mapper;

        _messenger.Register(this);

        Title = "Lista de Marcas";
        IsRefreshing = true;
    }

    public ObservableCollection<VehicleBrandPayload> VehicleBrands { get; } = new();

    public void Receive(VehicleBrandChangedMessage message)
    {
        MainThread.BeginInvokeOnMainThread(() => { IsRefreshing = true; });
        
    }

    [RelayCommand]
    async Task GetVehicleBrandsAsync()
    {

        try
        {
            if (_connectivity.NetworkAccess == NetworkAccess.None || _connectivity.NetworkAccess == NetworkAccess.Unknown)
            {
                await Shell.Current.DisplayAlert("Sem Conexão!",
                    $"Revise sua Conexão com a Rede Local.", "OK");
                return;
            }

            using AsyncServerStreamingCall<VehicleBrandPayload> call = _brandService.FindAllVehicleBrands(
                    new FindAllVehicleBrandsRequest
                    {
                        OnlyEnabled = true
                    });

            if (VehicleBrands.Count != 0)
                VehicleBrands.Clear();

            while (await call.ResponseStream.MoveNext())
            {
                VehicleBrands.Add(call.ResponseStream.Current);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get Brands: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToVehicleBrandAsync(VehicleBrandPayload vehicleBrand = null)
    {
        await Shell.Current.GoToAsync(nameof(VehicleBrandPage), true, new Dictionary<string, object>
        {
            {"VehicleBrand", vehicleBrand ?? new(){ Enabled = true }}
        });
    }
}