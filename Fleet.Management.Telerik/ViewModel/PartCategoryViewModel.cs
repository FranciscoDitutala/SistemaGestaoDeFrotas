using DynamicData;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(PartCategory), nameof(PartCategory))]
public partial class PartCategoryViewModel : BaseViewModel<PartCategoryViewModel>
{
    readonly PartManager.PartManagerClient _partService;

    public PartCategoryViewModel(PartManager.PartManagerClient partService)
    {
        _partService = partService;
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PartCategoryLoaded))]
    private PartCategoryPayload partCategory;

    partial void OnPartCategoryChanged(PartCategoryPayload value)
    {
        Title = $"Categoria: {value.Name}";
        IsRefreshing = true; // Load PartTypes
    }

    public bool PartCategoryLoaded => PartCategory is not null;

    public ObservableCollection<IGrouping<string, PartTypePayload>> PartTypes { get; } = new();

    [RelayCommand]
    async Task GetPartTypesAsync()
    {

        try
        {
            using AsyncServerStreamingCall<PartTypePayload> call = _partService.FindTypesByCategory(
                    new FindTypesByCategoryRequest
                    {
                        PartCategoryName = PartCategory.Name,
                        Filter = string.Empty
                    });

            if (PartTypes.Count != 0)
                PartTypes.Clear();

            var types = new List<PartTypePayload>();
            while (await call.ResponseStream.MoveNext())
            {
                types.Add(call.ResponseStream.Current);
            }
            PartTypes.AddRange(types
                .GroupBy(t => string.IsNullOrWhiteSpace(t.SubCategory) ? PartCategory.Name : t.SubCategory)
                .OrderBy(t => t.Key));
        }
        catch (Exception ex)
        {

            Debug.WriteLine($"Unable to get PartTypes: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsRefreshing = false;
        }
    }
}
