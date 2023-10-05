using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

public partial class StockEntryViewModel : BaseViewModel<StockEntryViewModel>
{
    StockEntryManager.StockEntryManagerClient _stockEntryService;
    readonly PartManager.PartManagerClient _partService;
    readonly IMessenger _messenger;
    private readonly IMapper _mapper;

    public StockEntryViewModel(
        StockEntryManager.StockEntryManagerClient stockEntryService,
        PartManager.PartManagerClient partService,
        IMessenger messenger,
        IMapper mapper)
    {
        _stockEntryService = stockEntryService;
        _partService = partService;
        _messenger = messenger;
        _mapper = mapper;

        Title = "Entrada de Stock";
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StockEntryLoaded), nameof(IsNew),
    nameof(RegisteredDate), nameof(RegisteredBy), nameof(LastUpdated),
    nameof(LastUpdatedBy), nameof(ProvidersInfo), nameof(Notes),
    nameof(TotalValue), nameof(Lines), nameof(Documents))]
    StockEntryPayload stockEntry;

    public bool StockEntryLoaded => StockEntry is not null;
    public bool IsNew => StockEntryLoaded && StockEntry.Id == 0;

    public DateTime? RegisteredDate => StockEntry?.RegisteredDate?.ToDateTime();
    public string RegisteredBy => StockEntry?.RegisteredBy;
    public DateTime? LastUpdated  => StockEntry?.LastUpdated?.ToDateTime();
    public string LastUpdatedBy => StockEntry?.LastUpdatedBy;


    public string ProvidersInfo
    {
        get => StockEntry?.ProvidersInfo;
        set
        {
            if (StockEntryLoaded)
                SetProperty(StockEntry.ProvidersInfo, value, StockEntry, (se, v) => { se.ProvidersInfo = v; });
        }
    }

    public string Notes
    {
        get => StockEntry?.Notes;
        set
        {
            if (StockEntryLoaded)
                SetProperty(StockEntry.Notes, value, StockEntry, (se, v) => { se.Notes = v; });
        }
    }

    public string TotalValue
    {
        get => $"{(decimal?)StockEntry?.TotalValue}";
        set
        {
            if (StockEntryLoaded && decimal.TryParse(value, out decimal decimalValue))
                SetProperty((decimal)StockEntry.TotalValue, decimalValue, StockEntry, (se, v) => { se.TotalValue = v; });
        }
    }

    public IEnumerable<StockEntryLinePayload> Lines => StockEntry?.Lines ?? new();

    public IEnumerable<DocumentMetadataPayload> Documents => StockEntry?.Documents ?? new();


    [ObservableProperty]
    string lineCode;

    [ObservableProperty]
    string lineQuantity;

    [ObservableProperty]
    PartPayload linePart;

    async Task AddLineAsync()
    {
        if(decimal.TryParse(LineQuantity, out decimal decimalQuantity) && decimalQuantity > 0.0m)
        {
            LinePart = await _partService.FindAsync(new() { Code = LineCode });
            if (LinePart is not null)
            {
                Lines.Append(new() { PartUPC = LinePart.UPC, Quantity = decimalQuantity });
                OnPropertyChanged(nameof(Lines));

                ClearNewLineForm();
            }
        }
        
    }

    void ClearNewLineForm()
    {
        LineCode = string.Empty;
        LineQuantity = string.Empty;
        LinePart = null;
    }

    [RelayCommand]
    async Task SaveStockEntryAsync()
    {
        try
        {
            if (!StockEntryLoaded)
                return;

            if (!await ShowErrors())
            {
                var msg = IsNew ?
                    $"Entrada de Stock Criada com Sucesso" :
                    $"Entrada de Stock Actualizada com Sucesso";

                StockEntry = IsNew ?
                    await _stockEntryService.CreateAsync(_mapper.Map<CreateStockEntryRequest>(StockEntry)) :
                    await _stockEntryService.UpdateAsync(_mapper.Map<UpdateStockEntryRequest>(StockEntry));
                IsChanged = false;

                _messenger.Send(new StockEntryChangedMessage(StockEntry));

                await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Save the Stock Entry: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
