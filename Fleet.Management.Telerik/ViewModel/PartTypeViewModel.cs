using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(PartType), nameof(PartType))]
public partial class PartTypeViewModel : BaseViewModel<PartTypeViewModel>, IRecipient<PartChangedMessage>
{
    readonly PartManager.PartManagerClient _partService;
    readonly IMessenger _messenger;

    public PartTypeViewModel(PartManager.PartManagerClient partService, IMessenger messenger)
    {
        _partService = partService;
        _messenger = messenger;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PartTypeLoaded))]
    private PartTypePayload partType;

    partial void OnPartTypeChanged(PartTypePayload value)
    {
        Title = $"Tipo de Peça: {value.Name}";
        IsRefreshing = true; // Load Parts
    }

    public bool PartTypeLoaded => PartType is not null;

    public ObservableCollection<PartPayload> Parts { get; } = new();

    [RelayCommand]
    async Task GetPartsAsync()
    {

        try
        {
            using AsyncServerStreamingCall<PartPayload> call = _partService.FindAllByType(
                    new FindPartsByTypeRequest
                    {
                        PartTypeName = PartType.Name,
                        Filter = string.Empty
                    });

            if (Parts.Count != 0)
                Parts.Clear();

            while (await call.ResponseStream.MoveNext())
            {
                Parts.Add(call.ResponseStream.Current);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get Parts: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToPartAsync(PartPayload part)
    {
        if (part is null)
            return;

        await Shell.Current.GoToAsync(nameof(PartPage), true, new Dictionary<string, object>
        {
            {nameof(PartViewModel.IsNew), false},
            {nameof(PartViewModel.Part), part }
        });
    }

    [ObservableProperty]
    string newPartUPC;

    [RelayCommand]
    async Task GoToNewPartAsync()
    {
        if (string.IsNullOrWhiteSpace(NewPartUPC))
            return;

        await Shell.Current.GoToAsync(nameof(PartPage), true, new Dictionary<string, object>
        {
            {nameof(PartViewModel.IsNew), true},
            {nameof(PartViewModel.Part), new PartPayload() { UPC = NewPartUPC }}
        });
    }

    public void Receive(PartChangedMessage message)
    {
        IsRefreshing = true;
    }
}
