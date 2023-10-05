using AutoMapper;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(BrandLogo), nameof(BrandLogo))]
[QueryProperty(nameof(BrandName), nameof(BrandName))]
[QueryProperty(nameof(ModelAcronym), nameof(ModelAcronym))]
[QueryProperty(nameof(VehicleVariant), nameof(VehicleVariant))]
public partial class VehicleVariantViewModel : BaseViewModel<VehicleVariantViewModel>
{
    readonly VehicleVariantManager.VehicleVariantManagerClient _variantService;
    readonly IMessenger _messenger;
    private readonly IMapper _mapper;

    public VehicleVariantViewModel(
        VehicleVariantManager.VehicleVariantManagerClient variantService,
        IMessenger messenger,
        IMapper mapper)
    {
        _variantService = variantService;
        _messenger = messenger;
        _mapper = mapper;

        Title = "Detalhes da Variante";
        IsRefreshing = true;
    }

    static VehicleVariantViewModel()
    {
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(VehicleVariant),
            "Objeto não Inicializado",
            x => x.VariantLoaded
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(Name),
            "O Nome da Variante é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.Name)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(Name),
            "O Nome da Variante não pode exceder os 120 caracteres",
            x => x.Name?.Length <= 120
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(TechnicalName),
            "O Nome Técnico da Variante é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.TechnicalName)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(TechnicalName),
            "O Nome Técnico da Variante não pode exceder os 30 caracteres",
            x => x.TechnicalName?.Length <= 30
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(ReleaseYear),
            "O Ano de Fabrico é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.ReleaseYear)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(ReleaseYear),
            $"O Ano de Fabrico é um Numero Inteiro no Intervalo [1975 - {DateTime.Now.Year + 1}]",
            x => int.TryParse(x.ReleaseYear, out int value) && value >= 1975 && value <= DateTime.Now.Year + 1
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(NumDoors),
            "O Número de Portas é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.ReleaseYear)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(NumDoors),
            "O Número de Portas é um Inteiro no Intervalo [0 - 10]",
            x => int.TryParse(x.NumDoors, out int value) && value >= 0 && value <= 10
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(ConsumptionKmL),
            "O Consumo (Km/L) é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.ReleaseYear)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(ConsumptionKmL),
            "O Consumo (Km/L) é um Número no Intervalo [0.00 - 999.99]",
            x => decimal.TryParse(x.ConsumptionKmL, out decimal value) && value >= 0.00m && value <= 999.99m
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(NumCylinders),
            "O Número Cilindros é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.ReleaseYear)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(NumCylinders),
            "O Número de Cilindros é um Inteiro no Intervalo [0 - 99]",
            x => int.TryParse(x.NumCylinders, out int value) && value >= 0 && value <= 99
            ));

        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(TransmissionSpeeds),
            "O Número de Velocidades é obrigatório",
            x => !string.IsNullOrWhiteSpace(x.ReleaseYear)
            ));
        Rules.Add(new DelegateRule<VehicleVariantViewModel>(
            nameof(TransmissionSpeeds),
            "O Número de Velocidades é um Inteiro no Intervalo [1 - 99]",
            x => int.TryParse(x.TransmissionSpeeds, out int value) && value >= 1 && value <= 99
            ));
    }

    [ObservableProperty]
    byte[] brandLogo;

    [ObservableProperty]
    string brandName;

    [ObservableProperty]
    string modelAcronym;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(VariantLoaded), nameof(IsNew),
        nameof(Name), nameof(TechnicalName), nameof(ReleaseYear),
        nameof(BodyStyle), nameof(NumDoors), nameof(FuelType),
        nameof(ConsumptionKmL), nameof(NumCylinders), nameof(CylinderArrangement),
        nameof(TransmissionType), nameof(TransmissionSpeeds), nameof(DrivertrainType))]
    VehicleVariantPayload vehicleVariant;

    public bool VariantLoaded => VehicleVariant is not null;
    public bool IsNew => VariantLoaded && VehicleVariant.Id == 0;

    public string ReleaseYear
    {
        get => $"{VehicleVariant?.ReleaseYear}";
        set
        {
            if (VariantLoaded && int.TryParse(value, out int intValue))
                SetProperty(VehicleVariant.ReleaseYear, intValue, VehicleVariant, (vv, v) => { vv.ReleaseYear = v; });
        }
    }

    public string Name
    {
        get => VehicleVariant?.Name;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.Name, value, VehicleVariant, (vv, v) => { vv.Name = v; });
        }
    }

    public string TechnicalName
    {
        get => VehicleVariant?.TechnicalName;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.TechnicalName, value, VehicleVariant, (vv, v) => { vv.TechnicalName = v; });
        }
    }

    #region BodySpecs
    public object[] BodyStyles { get; } = ClientTools.GetEnumValues(typeof(VehicleBodyStyleRepresentation));
    public VehicleBodyStyleRepresentation BodyStyle
    {
        get => VehicleVariant?.BodySpecs.BodyStyle ?? VehicleBodyStyleRepresentation.None;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.BodySpecs.BodyStyle, value, VehicleVariant.BodySpecs, (bs, v) => { bs.BodyStyle = v; });
        }
    }

    public string NumDoors
    {
        get => $"{VehicleVariant?.BodySpecs.NumDoors}";
        set
        {
            if (VariantLoaded && int.TryParse(value, out int intValue))
                SetProperty(VehicleVariant.BodySpecs.NumDoors, intValue, VehicleVariant.BodySpecs, (bs, v) => { bs.NumDoors = v; });
        }
    }
    #endregion BodySpecs

    #region TechnicalSpecs
    public object[] FuelTypes { get; } = ClientTools.GetEnumValues(typeof(EngineFuelTypeRepresentation));
    public EngineFuelTypeRepresentation FuelType
    {
        get => VehicleVariant?.TechnicalSpecs.FuelType ?? EngineFuelTypeRepresentation.None;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.TechnicalSpecs.FuelType, value, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.FuelType = v; });
        }
    }

    public string ConsumptionKmL
    {
        get => $"{(decimal?)VehicleVariant?.TechnicalSpecs.ConsumptionKmL}";
        set
        {
            if (VariantLoaded && decimal.TryParse(value, out decimal decimalValue))
                SetProperty((decimal)VehicleVariant.TechnicalSpecs.ConsumptionKmL, decimalValue, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.ConsumptionKmL = v; });
        }
    }

    public string NumCylinders
    {
        get => $"{VehicleVariant?.TechnicalSpecs.NumCylinders}";
        set
        {
            if (VariantLoaded && int.TryParse(value, out int intValue))
                SetProperty(VehicleVariant.TechnicalSpecs.NumCylinders, intValue, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.NumCylinders = v; });
        }
    }

    public object[] CylinderArrangements { get; } = ClientTools.GetEnumValues(typeof(EngineCylinderArrangementRepresentation));
    public EngineCylinderArrangementRepresentation CylinderArrangement
    {
        get => VehicleVariant?.TechnicalSpecs.CylinderArrangement ?? EngineCylinderArrangementRepresentation.None;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.TechnicalSpecs.CylinderArrangement, value, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.CylinderArrangement = v; });
        }
    }

    public object[] TransmissionTypes { get; } = ClientTools.GetEnumValues(typeof(VehicleTransmissionTypeRepresentation));
    public VehicleTransmissionTypeRepresentation TransmissionType
    {
        get => VehicleVariant?.TechnicalSpecs.TransmissionType ?? VehicleTransmissionTypeRepresentation.None;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.TechnicalSpecs.TransmissionType, value, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.TransmissionType = v; });
        }
    }

    public string TransmissionSpeeds
    {
        get => $"{VehicleVariant?.TechnicalSpecs.TransmissionSpeeds}";
        set
        {
            if (VariantLoaded && int.TryParse(value, out int intValue))
                SetProperty(VehicleVariant.TechnicalSpecs.TransmissionSpeeds, intValue, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.TransmissionSpeeds = v; });
        }
    }

    public object[] DrivertrainTypes { get; } = ClientTools.GetEnumValues(typeof(VehicleDrivertrainTypeRepresentation));
    public VehicleDrivertrainTypeRepresentation DrivertrainType
    {
        get => VehicleVariant?.TechnicalSpecs.DrivertrainType ?? VehicleDrivertrainTypeRepresentation.None;
        set
        {
            if (VariantLoaded)
                SetProperty(VehicleVariant.TechnicalSpecs.DrivertrainType, value, VehicleVariant.TechnicalSpecs, (ts, v) => { ts.DrivertrainType = v; });
        }
    }
    #endregion TechnicalSpecs


    [RelayCommand]
    async Task SaveVehicleVariantAsync()
    {
        try
        {
            if (!VariantLoaded)
                return;

            if(!await ShowErrors())
            {
                var msg = IsNew ?
                    $"Variante {VehicleVariant.Name} Criada com Sucesso" :
                    $"Variante {VehicleVariant.Name} Actualizada com Sucesso";

                VehicleVariant = IsNew ?
                    await _variantService.CreateVariantAsync(_mapper.Map<CreateVehicleVariantRequest>(VehicleVariant)) :
                    await _variantService.UpdateVariantAsync(VehicleVariant);

                IsChanged = false;

                _messenger.Send(new VehicleVariantChangedMessage(VehicleVariant));

                await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Save the Vehicle Variant: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
