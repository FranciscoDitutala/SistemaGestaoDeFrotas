using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

public partial class StockEntriesViewModel : BaseViewModel<StockEntriesViewModel>
{
    StockEntryManager.StockEntryManagerClient _stockEntryService;

    public StockEntriesViewModel(StockEntryManager.StockEntryManagerClient stockEntryService)
    {
        _stockEntryService = stockEntryService;

        Title = "Entradas";
        IsRefreshing = true;
    }

    public ObservableCollection<StockEntryPayload> StockEntries { get; } = new();

    [RelayCommand]
    async Task GetStockEntriesAsync()
    {

        try
        {
            using AsyncServerStreamingCall<StockEntryPayload> call = _stockEntryService.FindAll(
                    new FindStockEntriesRequest
                    {
                        ByDateRange = false,
                    });

            if (StockEntries.Count != 0)
                StockEntries.Clear();

            while (await call.ResponseStream.MoveNext())
            {
                StockEntries.Add(call.ResponseStream.Current);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get Stock Entries: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToStockEntryAsync(StockEntryPayload stockEntry)
    {
        await Shell.Current.GoToAsync(nameof(StockEntryPage), true, new Dictionary<string, object>
        {
            { "StockEntry", stockEntry ?? new StockEntryPayload() }
        });
    }
}
