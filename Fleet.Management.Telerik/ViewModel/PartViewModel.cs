using AutoMapper;
using System.Diagnostics;

namespace Fleet.Management.ViewModel;

[QueryProperty(nameof(IsNew), nameof(IsNew))]
[QueryProperty(nameof(Part), nameof(Part))]
public partial class PartViewModel : BaseViewModel<PartViewModel>
{
    private readonly IMessenger _messenger;
    private readonly IMapper _mapper;
    readonly PartManager.PartManagerClient _partService;

    private static HashSet<string> partTypesNames = new();

    public PartViewModel(IMessenger messenger, IMapper mapper, PartManager.PartManagerClient partService)
    {
        _messenger = messenger;
        _mapper = mapper;
        _partService = partService;

        if(!partTypesNames.Any())
        {
            InitializeAsync();
        }
    }

    async void InitializeAsync()
    {
        try
        {
            using AsyncServerStreamingCall<PartCategoryPayload> categoriesCall = _partService.FindCategories(
                    new FindCategoriesRequest
                    {
                        Filter = string.Empty
                    });

            if (partTypesNames.Any())
                partTypesNames.Clear();

            var categories = new List<PartCategoryPayload>();
            while (await categoriesCall.ResponseStream.MoveNext())
            {
                categories.Add(categoriesCall.ResponseStream.Current);
            }

            foreach(var category in categories)
            {
                using AsyncServerStreamingCall<PartTypePayload> typesCall = _partService.FindTypesByCategory(
                    new FindTypesByCategoryRequest
                    {
                        PartCategoryName = category.Name,
                        Filter = string.Empty
                    });

                
                while (await typesCall.ResponseStream.MoveNext())
                {
                    partTypesNames.Add(typesCall.ResponseStream.Current.Name);
                }
            }
            OnPropertyChanged(nameof(PartTypeNames));
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get all part types: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PartLoaded),
        nameof(UPC), nameof(PartTypeName), nameof(Description),
        nameof(SKU), nameof(Brand), nameof(Model))]
    PartPayload part;

    public bool PartLoaded => Part is not null;

    [ObservableProperty]
    bool isNew;

    public string UPC
    {
        get => Part?.UPC;
        set
        {
            if (PartLoaded)
                SetProperty(Part.UPC, value, Part, (p, v) => { p.UPC = v; });
        }
    }

    public List<string> PartTypeNames => partTypesNames.OrderBy(n => n).ToList();

    public string PartTypeName
    {
        get => Part?.PartTypeName;
        set
        {
            if (PartLoaded)
                SetProperty(Part.PartTypeName, value, Part, (p, v) => { p.PartTypeName = v; });
        }
    }

    public string Description
    {
        get => Part?.Description;
        set
        {
            if (PartLoaded)
                SetProperty(Part.Description, value, Part, (p, v) => { p.Description = v; });
        }
    }

    public string SKU
    {
        get => Part?.SKU;
        set
        {
            if (PartLoaded)
                SetProperty(Part.SKU, value, Part, (p, v) => { p.SKU = v; });
        }
    }

    public string Brand
    {
        get => Part?.Brand;
        set
        {
            if (PartLoaded)
                SetProperty(Part.Brand, value, Part, (p, v) => { p.Brand = v; });
        }
    }

    public string Model
    {
        get => Part?.Model;
        set
        {
            if (PartLoaded)
                SetProperty(Part.Model, value, Part, (p, v) => { p.Model = v; });
        }
    }

    [RelayCommand]
    async Task SavePartAsync()
    {
        try
        {
            if (!PartLoaded)
                return;

            if (!await ShowErrors())
            {
                var msg = IsNew ?
                    $"Peça \"{Part.Description}\" Criada com Sucesso" :
                    $"Peça \"{Part.Description}\" Actualizada com Sucesso";

                Part = IsNew ?
                    await _partService.CreateAsync(_mapper.Map<CreatePartRequest>(Part)) :
                    await _partService.UpdateAsync(Part);
                IsChanged = false;

                _messenger.Send(new PartChangedMessage(Part));

                await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Save the Part: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
