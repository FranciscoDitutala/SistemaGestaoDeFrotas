using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

public partial class PartCategoriesViewModel : BaseViewModel<PartCategoriesViewModel>
{
    readonly PartManager.PartManagerClient _partService;

    public PartCategoriesViewModel(PartManager.PartManagerClient partService)
    {
        _partService = partService;

        Title = "Categorias";
        IsRefreshing = true;

        //Task.WhenAll(GetPartCategoriesAsync());
    }

    public ObservableCollection<PartCategoryPayload> PartCategories { get; } = new();

    [RelayCommand]
    async Task GetPartCategoriesAsync()
    {

        try
        {
            using AsyncServerStreamingCall<PartCategoryPayload> call = _partService.FindCategories(
                    new FindCategoriesRequest
                    {
                        Filter = string.Empty
                    });

            if (PartCategories.Count != 0)
                PartCategories.Clear();

            while (await call.ResponseStream.MoveNext())
            {
                PartCategories.Add(call.ResponseStream.Current);
            }
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get Categories: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task GoToPartCategoryAsync(PartCategoryPayload partCategory)
    {
        if(partCategory is null) return;
        await Shell.Current.GoToAsync(nameof(PartCategoryPage), true, new Dictionary<string, object>
        {
            {"PartCategory", partCategory}
        });
    }
}
