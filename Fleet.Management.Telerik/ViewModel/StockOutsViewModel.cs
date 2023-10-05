
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

public partial class StockOutsViewModel : BaseViewModel<StockOutsViewModel>
{
    readonly StockOutManager.StockOutManagerClient _stockOutService;

    public StockOutsViewModel(StockOutManager.StockOutManagerClient stockOutService)
    {
        _stockOutService = stockOutService;

        Title = "Saídas";
        IsRefreshing = true;
    }

    public ObservableCollection<StockOutPayload> StockOuts { get; } = new();

    [RelayCommand]
    async Task GetStockOutsAsync()
    {

        try
        {
            using AsyncServerStreamingCall<StockOutPayload> call = _stockOutService.FindAll(
                    new FindAllStockOutRequest
                    {
                        ByDateRange = false,
                    });

            if (StockOuts.Count != 0)
                StockOuts.Clear();

            while (await call.ResponseStream.MoveNext())
            {
                StockOuts.Add(call.ResponseStream.Current);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get Stock Out Kist: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToStockOutAsync(StockOutPayload stockOut)
    {
        await Shell.Current.GoToAsync(nameof(StockOutPage), true, new Dictionary<string, object>
        {
            { nameof(StockOutViewModel.StockOut), stockOut ?? new StockOutPayload() }
        });
    }
}
