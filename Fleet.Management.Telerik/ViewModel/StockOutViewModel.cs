
using AutoMapper;
using DynamicData;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(StockOut), nameof(StockOut))]
public partial class StockOutViewModel : BaseViewModel<StockOutViewModel>
{
    readonly StockOutManager.StockOutManagerClient _stockOutService;
    readonly PartManager.PartManagerClient _partService;
    readonly IMessenger _messenger;
    private readonly IMapper _mapper;

    public StockOutViewModel(
        StockOutManager.StockOutManagerClient stockOutService,
        PartManager.PartManagerClient partService,
        IMessenger messenger,
        IMapper mapper)
    {
        _stockOutService = stockOutService;
        _partService = partService;
        _messenger = messenger;
        _mapper = mapper;

        Title = "Saída de Stock";
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(StockOutLoaded), nameof(IsNew), nameof(Notes),
        nameof(RegisteredDate), nameof(RegisteredBy),
        nameof(LastUpdated), nameof(LastUpdatedBy),
        nameof(ApprovedDate), nameof(ApprovedBy),
        nameof(CancelledDate), nameof(CancelledBy),
        nameof(DeliveredDate), nameof(DeliveredBy),
        nameof(RequestedBy), nameof(DeliveredTo),
        nameof(RequestedLines), nameof(ApprovedLines), nameof(CurrentLines),
        nameof(Documents), nameof(CanApproveOrCancel), nameof(CanAddLineAndSave))]
    StockOutPayload stockOut;

    partial void OnStockOutChanged(StockOutPayload value)
    {
        UpdateLines(value);
    }

    public bool StockOutLoaded => StockOut is not null;
    public bool IsNew => StockOutLoaded && StockOut.Id == 0;

    public DateTime? RegisteredDate => StockOut?.RegisteredDateValue;
    public string RegisteredBy => StockOut?.RegisteredBy;
    public DateTime? LastUpdated => StockOut?.LastUpdatedValue;
    public string LastUpdatedBy => StockOut?.LastUpdatedBy;

    public DateTime? ApprovedDate => StockOut?.ApprovedDateValue;
    public string ApprovedBy => StockOut?.ApprovedBy;
    public DateTime? CancelledDate => StockOut?.CancelledDateValue;
    public string CancelledBy => StockOut?.CancelledBy;
    public DateTime? DeliveredDate => StockOut?.DeliveredDateValue;
    public string DeliveredBy => StockOut?.DeliveredBy;

    public bool Approved => StockOut?.Approved ?? false;
    public bool Delivered => StockOut?.Delivered ?? false;
    public bool Cancelled => StockOut?.Cancelled ?? false;


    public string RequestedBy
    {
        get => StockOut?.RequestedBy;
        set
        {
            if (StockOutLoaded)
                SetProperty(StockOut.RequestedBy, value, StockOut, (so, v) => { so.RequestedBy = v; });
        }
    }

    public string DeliveredTo
    {
        get => StockOut?.DeliveredTo;
        set
        {
            if (StockOutLoaded)
                SetProperty(StockOut.DeliveredTo, value, StockOut, (so, v) => { so.DeliveredTo = v; });
        }
    }

    public string Notes
    {
        get => StockOut?.Notes;
        set
        {
            if (StockOutLoaded)
                SetProperty(StockOut.Notes, value, StockOut, (so, v) => { so.Notes = v; });
        }
    }

    public ObservableCollection<StockOutLinePayload> RequestedLines { get; } = new();
    public ObservableCollection<StockOutLinePayload> ApprovedLines { get; } = new();
    public ObservableCollection<StockOutLinePayload> CurrentLines => IsNew || Cancelled ? RequestedLines : ApprovedLines;

    private void UpdateLines()
    {
        UpdateLines(StockOut);
    }

    private void UpdateLines(StockOutPayload value)
    {
        if (value is not null)
        {
            RequestedLines.Clear();
            RequestedLines.AddRange(value.RequestedLines);

            ApprovedLines.Clear();
            if (value.Approved)
                ApprovedLines.AddRange(value.ApprovedLines);
            else if (!value.Cancelled)
                ApprovedLines.AddRange(value.RequestedLines);

            OnPropertyChanged(nameof(CurrentLines));
            OnPropertyChanged(nameof(CanApproveOrCancel));
            OnPropertyChanged(nameof(CanAddLineAndSave));
        }
    }

    public ObservableCollection<DocumentMetadataPayload> Documents { get; } = new();


    [ObservableProperty]
    string lineCode;

    [ObservableProperty]
    double? lineQuantity;

    [ObservableProperty]
    PartPayload linePartPayload;

    public bool CanAddLineAndSave => IsNew || CanApproveOrCancel;
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
                await Shell.Current.DisplayAlert("Código Inválido", $"O Código \"{LineCode}\"\nnão corresponde á nenhum material no stock.", "OK");
            }
        }
    }

    private async Task AddLine(decimal decimalQuantity)
    {
        var line = StockOut.RequestedLines.FirstOrDefault(l => l.PartUPC == LinePartPayload.UPC);
        if (line is null)
        {
            if (decimalQuantity > 0)
            {
                StockOut.RequestedLines.Add(new StockOutLinePayload()
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
                        "Não é permitida uma quantidade negativa\npara novas saidas", "OK");
            }
        }
        else
        {
            if (line.Quantity + decimalQuantity > 0)
            {
                line.Description = LinePartPayload.Description;
                line.PartTypeName = LinePartPayload.PartTypeName;
                line.Quantity += decimalQuantity;

                UpdateLines();
                ClearNewLineForm();
            }
            else if (line.Quantity + decimalQuantity == 0)
            {
                StockOut.RequestedLines.Remove(line);

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

    void ClearNewLineForm()
    {
        LineCode = string.Empty;
        LineQuantity = null;
        LinePartPayload = null;
    }

    public bool CanApproveOrCancel => StockOutLoaded && !IsNew && !Approved && !Cancelled;

    [RelayCommand]
    async Task ApproveAsync()
    {
        try
        {
            if (!StockOutLoaded)
                return;

            if (!await ShowErrors())
            {
                var msg = $"Saida de Stock Approvada com Sucesso";

                var request = new StockOutApproveRequest
                {
                    Id = StockOut.Id
                };
                request.ApprovedLines.AddRange(CurrentLines);
                StockOut = await _stockOutService.ApproveAsync(request);

                if (Approved)
                {
                    IsChanged = false;

                    _messenger.Send(new StockOutChangedMessage(StockOut));

                    await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
                }
                else
                {
                    await Shell.Current.DisplayAlert("Dados Irregulares", "Verifique se tem suficiente Stock.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Approve Stock Out: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task SaveStockOutAsync()
    {
        try
        {
            if (!StockOutLoaded)
                return;

            if (!await ShowErrors())
            {
                var msg = IsNew ?
                    $"Saida de Stock Criada com Sucesso" :
                    $"Saida de Stock Actualizada com Sucesso";

                StockOut = IsNew ?
                    await _stockOutService.CreateAsync(_mapper.Map<CreateStockOutRequest>(StockOut)) :
                    await _stockOutService.UpdateAsync(_mapper.Map<UpdateStockOutRequest>(StockOut));
                IsChanged = false;

                _messenger.Send(new StockOutChangedMessage(StockOut));

                await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Save the Stock Out: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
