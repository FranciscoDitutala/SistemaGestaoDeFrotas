using AutoMapper;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(VehicleBrand), nameof(VehicleBrand))]
public partial class VehicleBrandViewModel : BaseViewModel<VehicleBrandViewModel>, IRecipient<VehicleModelChangedMessage>
{
    readonly VehicleBrandManager.VehicleBrandManagerClient _brandService;
    readonly VehicleModelManager.VehicleModelManagerClient _modelService;
    readonly IMessenger _messenger;
    readonly IMapper _mapper;

    public VehicleBrandViewModel(
        VehicleBrandManager.VehicleBrandManagerClient brandService,
        VehicleModelManager.VehicleModelManagerClient modelService,
        IMessenger messenger,
        IMapper mapper)
    {
        _brandService = brandService;
        _modelService = modelService;
        _messenger = messenger;
        _mapper = mapper;

        _messenger.Register(this);

        Title = "Detalhes da Marca";

        
    }

    static VehicleBrandViewModel()
    {
        Rules.Add(new DelegateRule<VehicleBrandViewModel>(
            nameof(VehicleBrand),
            "Objeto não Inicializado",
            x => x.BrandLoaded
            ));

        Rules.Add(new DelegateRule<VehicleBrandViewModel>(
            nameof(Company),
            "A Companhia é obrigatória",
            x => !string.IsNullOrWhiteSpace(x.Company)
            ));
        Rules.Add(new DelegateRule<VehicleBrandViewModel>(
            nameof(Company),
            "O Nome da Companhia não pode exceder os 120 caracteres",
            x => x.Company?.Length <= 120
            ));

        Rules.Add(new DelegateRule<VehicleBrandViewModel>(
            nameof(Name),
            "O Nome da Marca é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.Name)
            ));
        Rules.Add(new DelegateRule<VehicleBrandViewModel>(
            nameof(Name),
            "O Nome da Marca não pode exceder os 30 caracteres",
            x => x.Name?.Length <= 30
            ));

        Rules.Add(new DelegateRule<VehicleBrandViewModel>(
           nameof(Logo),
           "Insira o Logotipo da Marca",
           x => x.LogoLoaded
           ));
    }


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(BrandLoaded), nameof(IsNew), nameof(EditEnabled), nameof(LogoLoaded), nameof(Logo), nameof(Company), nameof(Name))]
    VehicleBrandPayload vehicleBrand;

    partial void OnVehicleBrandChanged(VehicleBrandPayload value)
    {
        IsChanged = IsNew;
        IsRefreshing = true; // Load VehicleModels
    }

    public bool BrandLoaded => VehicleBrand is not null;
    public bool IsNew => BrandLoaded && VehicleBrand.Id == 0;
    public bool LogoLoaded => BrandLoaded && VehicleBrand.Logo is not null && !VehicleBrand.Logo.IsEmpty;
    public byte[] Logo => VehicleBrand?.LogoData;

    private bool editEnabled;
    public bool EditEnabled
    {
        get => IsNew || editEnabled; 
        set
        {
            SetProperty(ref editEnabled, value);
        }
    }


    public string Company
    {
        get => VehicleBrand?.Company;
        set
        {
            SetProperty(VehicleBrand.Company, value, VehicleBrand, (b, v) => { b.Company = v; });
        }
    }

    public string Name
    {
        get => VehicleBrand?.Name;
        set
        {
            SetProperty(VehicleBrand.Name, value, VehicleBrand, (b, v) => { b.Name = v; });
        }
    }

    [RelayCommand]
    async Task UpdateLogoAsync()
    {
        PickOptions options = new()
        {
            PickerTitle = "Escolha a Imagem do Logo",
            FileTypes = FilePickerFileType.Png
        };

        try
        {
            var logo = await FilePicker.Default.PickAsync(options);
            if (logo != null)
            {
                using var stream = await logo.OpenReadAsync();
                VehicleBrand.Logo = await Google.Protobuf.ByteString.FromStreamAsync(stream);
                OnPropertyChanged(nameof(LogoLoaded));
                OnPropertyChanged(nameof(Logo));
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to load the Logo: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task SaveVehicleBrandAsync()
    {
        try
        {
            if (!BrandLoaded)
                return;

            if (!await ShowErrors())
            {
                var msg = IsNew ? 
                    $"Marca {VehicleBrand.Name} Criada com Sucesso" :
                    $"Marca {VehicleBrand.Name} Actualizada com Sucesso";

                VehicleBrand = IsNew ?
               await _brandService.CreateBrandAsync(_mapper.Map<CreateVehicleBrandRequest>(VehicleBrand)) :
               await _brandService.UpdateBrandAsync(VehicleBrand);

               IsChanged = false;

               _messenger.Send(new VehicleBrandChangedMessage(VehicleBrand));

               await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Save the Vehicle Brand: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    #region BRAND MODELS
    public ObservableCollection<VehicleModelPayload> VehicleModels { get; } = new();

    void IRecipient<VehicleModelChangedMessage>.Receive(VehicleModelChangedMessage message)
    {
        IsRefreshing = true;
    }

    [RelayCommand]
    async Task GetVehicleModelsAsync()
    {
        try
        {
            if (!BrandLoaded || IsNew)
                return;

            using AsyncServerStreamingCall<VehicleModelPayload> call = _modelService.FindVehicleModelsByBrand(
                    new FindVehicleModelsByBrandRequest
                    {
                        VehicleBrandId = VehicleBrand.Id,
                        OnlyEnabled = true
                    });

            if (VehicleModels.Count != 0)
                VehicleModels.Clear();

            while (await call.ResponseStream.MoveNext())
            {
                VehicleModels.Add(call.ResponseStream.Current);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get Models: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToVehicleModelAsync(VehicleModelPayload vehicleModel = null)
    {
        if (!BrandLoaded || IsNew)
            return;

        if (!await ShowErrors())
        {
            await Shell.Current.GoToAsync(nameof(VehicleModelPage), true, new Dictionary<string, object>
            {
                {nameof(VehicleModelViewModel.BrandLogo), Logo},
                {nameof(VehicleModelViewModel.BrandName), Name},
                {nameof(VehicleModelViewModel.VehicleModel), vehicleModel ?? new(){ Enabled = true, VehicleBrandId = VehicleBrand.Id }}
            });
        }
    }

    #endregion BRAND MODELS
}
