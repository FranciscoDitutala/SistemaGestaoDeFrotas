using AutoMapper;
using DynamicData;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(StockEntry), nameof(StockEntry))]
public partial class StockEntryViewModel : BaseViewModel<StockEntryViewModel>, IRecipient<PartChangedMessage>
{
    readonly StockEntryManager.StockEntryManagerClient _stockEntryService;
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

        _messenger.Register(this);

        Title = "Entrada de Stock";
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StockEntryLoaded), nameof(IsNew),
    nameof(RegisteredDate), nameof(RegisteredBy), nameof(LastUpdated),
    nameof(LastUpdatedBy), nameof(ProvidersInfo), nameof(Notes),
    nameof(TotalValue), nameof(Lines), nameof(Documents))]
    StockEntryPayload stockEntry;

    partial void OnStockEntryChanged(StockEntryPayload value)
    {
        Lines.Clear();
        Lines.AddRange(value.Lines);
    }


    public bool StockEntryLoaded => StockEntry is not null;
    public bool IsNew => StockEntryLoaded && StockEntry.Id == 0;

    public DateTime? RegisteredDate => StockEntry?.RegisteredDateValue;
    public string RegisteredBy => StockEntry?.RegisteredBy;
    public DateTime? LastUpdated  => StockEntry?.LastUpdatedValue;
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

    [Required]
    public double? TotalValue
    {
        get => (double?)(decimal?)StockEntry?.TotalValue;
        set
        {
            if (StockEntryLoaded)
                SetProperty((decimal)StockEntry.TotalValue, (decimal)value, StockEntry, (se, v) => { se.TotalValue = v; });
        }
    }

    public ObservableCollection<StockEntryLinePayload> Lines { get; } = new();
    private void UpdateLines()
    {
        Lines.Clear();
        if (StockEntryLoaded)
            Lines.AddRange(StockEntry.Lines);
    }

    public ObservableCollection<DocumentMetadataPayload> Documents { get; } = new();


    [ObservableProperty]
    string lineCode;

    [ObservableProperty]
    double? lineQuantity;

    [ObservableProperty]
    PartPayload linePartPayload;

    [RelayCommand]
    async Task AddLineAsync()
    {
        decimal decimalQuantity = (decimal?)LineQuantity ?? 0.00m;
        if (!string.IsNullOrWhiteSpace(LineCode) && decimalQuantity != 0.0m)
        {
            LinePartPayload = await _partService.FindAsync(new() { Code = LineCode });
            if (LinePartPayload is not null && !string.IsNullOrWhiteSpace(LinePartPayload.UPC))
            {
                LineCode = LinePartPayload.UPC;
                await AddLine(decimalQuantity);
            }
            else
            {
                LinePartPayload = new PartPayload() { UPC = LineCode };
                await GoToNewPartAsync();
            }
        }

    }

    private async Task AddLine(decimal decimalQuantity)
    {
        if (IsNew)
        {
            var line = StockEntry.Lines.FirstOrDefault(l => l.PartUPC == LinePartPayload.UPC);
            if (line is null)
            {
                if (decimalQuantity > 0)
                {
                    StockEntry.Lines.Add(new StockEntryLinePayload()
                    {
                        PartUPC = LinePartPayload.UPC,
                        Description = LinePartPayload.Description,
                        PartTypeName = LinePartPayload.PartTypeName,
                        Quantity = decimalQuantity
                    });

                    UpdateLines();
                    ClearNewLineForm();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Quantidade Negativa",
                        "Não é permitida uma quantidade negativa\npara novas entradas", "OK");
                }
            }
            else
            {
                if (line.Quantity + decimalQuantity >= 0)
                {
                    line.Description = LinePartPayload.Description;
                    line.PartTypeName = LinePartPayload.PartTypeName;
                    line.Quantity += decimalQuantity;

                    UpdateLines();
                    ClearNewLineForm();
                }
                else if (line.Quantity + decimalQuantity == 0)
                {
                    StockEntry.Lines.Remove(line);

                    UpdateLines();
                    ClearNewLineForm();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Quantidade Negativa",
                        "A quantidade total da linha náo pode ser negativa", "OK");
                }
            }
             
        }
    }

    public async void Receive(PartChangedMessage message)
    {
        decimal decimalQuantity = (decimal?)LineQuantity ?? 0.00m;
        if (message.Value.UPC == LinePartPayload?.UPC && decimalQuantity > 0.0m)
        {
            LinePartPayload = message.Value;
            await AddLine(decimalQuantity);
        }
    }
  
    async Task GoToNewPartAsync()
    {
        await Shell.Current.GoToAsync(nameof(PartPage), true, new Dictionary<string, object>
        {
            {nameof(PartViewModel.IsNew), true},
            {nameof(PartViewModel.Part), LinePartPayload}
        });
    }

    void ClearNewLineForm()
    {
        LineCode = string.Empty;
        LineQuantity = null;
        LinePartPayload = null;
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
