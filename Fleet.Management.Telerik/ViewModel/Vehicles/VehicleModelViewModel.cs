using AutoMapper;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.Management.ViewModel
{
    [QueryProperty(nameof(BrandLogo), nameof(BrandLogo))]
    [QueryProperty(nameof(BrandName), nameof(BrandName))]
    [QueryProperty(nameof(VehicleModel), nameof(VehicleModel))]
    public partial class VehicleModelViewModel : BaseViewModel<VehicleModelViewModel>, IRecipient<VehicleVariantChangedMessage>
    {

        readonly VehicleModelManager.VehicleModelManagerClient _modelService;
        readonly VehicleVariantManager.VehicleVariantManagerClient _variantService;
        readonly IMessenger _messenger;
        private readonly IMapper _mapper;

        public VehicleModelViewModel(
            VehicleModelManager.VehicleModelManagerClient modelService,
            VehicleVariantManager.VehicleVariantManagerClient variantService,
            IMessenger messenger,
            IMapper mapper)
        {
            _modelService = modelService;
            _variantService = variantService;
            _messenger = messenger;
            _mapper = mapper;

            _messenger.Register(this);

            Title = "Detalhes do Modelo";
        }

        static VehicleModelViewModel()
        {
            Rules.Add(new DelegateRule<VehicleModelViewModel>(
                nameof(VehicleModel),
                "Objeto não Inicializado",
                x => x.ModelLoaded
                ));

            Rules.Add(new DelegateRule<VehicleModelViewModel>(
                nameof(Name),
                "O Nome do Modelo é obrigatório",
                x => !string.IsNullOrWhiteSpace(x.Name)
                ));
            Rules.Add(new DelegateRule<VehicleModelViewModel>(
                nameof(Name),
                "O Nome do Modelo não pode exceder os 120 caracteres",
                x => x.Name?.Length <= 120
                ));

            Rules.Add(new DelegateRule<VehicleModelViewModel>(
                nameof(Acronym),
                "O Alias do Modelo é obrigatório",
                x => !string.IsNullOrWhiteSpace(x.Acronym)
                ));
            Rules.Add(new DelegateRule<VehicleModelViewModel>(
                nameof(Acronym),
                "O Alias do Modelo não pode exceder os 30 caracteres",
                x => x.Acronym?.Length <= 30
                ));
        }

        [ObservableProperty]
        byte[] brandLogo;

        [ObservableProperty]
        string brandName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ModelLoaded), nameof(IsNew), nameof(Name), nameof(Acronym))]
        VehicleModelPayload vehicleModel;

        partial void OnVehicleModelChanged(VehicleModelPayload value)
        {
            IsChanged = IsNew;
            IsRefreshing = true; // Load VehicleVariants
        }

        public bool ModelLoaded => VehicleModel is not null;
        public bool IsNew => ModelLoaded && VehicleModel.Id == 0;

        public string Name
        {
            get => VehicleModel?.Name;
            set
            {
                SetProperty(VehicleModel.Name, value, VehicleModel, (m, v) => { m.Name = v; });
            }
        }

        public string Acronym
        {
            get => VehicleModel?.Acronym;
            set
            {
                SetProperty(VehicleModel.Acronym, value, VehicleModel, (m, v) => { m.Acronym = v; });
            }
        }


        [RelayCommand]
        async Task SaveVehicleModelAsync()
        {
            try
            {
                if (!ModelLoaded)
                    return;

                if (!await ShowErrors())
                {
                    var msg = IsNew ?
                        $"Modelo {VehicleModel.Acronym} Criado com Sucesso" :
                        $"Modelo {VehicleModel.Acronym} Actualizado com Sucesso";

                    VehicleModel = IsNew ?
                        await _modelService.CreateModelAsync(_mapper.Map<CreateVehicleModelRequest>(VehicleModel)) :
                        await _modelService.UpdateModelAsync(VehicleModel);
                    
                    IsChanged = false;
                    
                    _messenger.Send(new VehicleModelChangedMessage(VehicleModel));

                    await Shell.Current.DisplayAlert("Operação Concluída", msg, "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to Save the Vehicle Model: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        #region MODEL VARIANTS
        public ObservableCollection<VehicleVariantPayload> VehicleVariants { get; } = new();

        void IRecipient<VehicleVariantChangedMessage>.Receive(VehicleVariantChangedMessage message)
        {
            IsRefreshing = true;
        }

        [RelayCommand]
        async Task GetVehicleVariantsAsync()
        {
            try
            {
                if (!ModelLoaded || IsNew)
                    return;

                using AsyncServerStreamingCall<VehicleVariantPayload> call = _variantService.FindVehicleVariantsByModel(
                        new FindVehicleVariantsByModelRequest
                        {
                            VehicleModelId = VehicleModel.Id,
                            OnlyEnabled = true
                        });

                if (VehicleVariants.Count != 0)
                    VehicleVariants.Clear();

                while (await call.ResponseStream.MoveNext())
                {
                    VehicleVariants.Add(call.ResponseStream.Current);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Unable to get Variants: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GoToVehicleVariantAsync(VehicleVariantPayload vehicleVariant = null)
        {
            if (!ModelLoaded || IsNew)
                return;

            if (!await ShowErrors())
            {
                await Shell.Current.GoToAsync(nameof(VehicleVariantPage), true, new Dictionary<string, object>
                {
                    {nameof(VehicleVariantViewModel.BrandLogo), BrandLogo},
                    {nameof(VehicleVariantViewModel.BrandName), BrandName},
                    {nameof(VehicleVariantViewModel.ModelAcronym), VehicleModel.Acronym},
                    {nameof(VehicleVariantViewModel.VehicleVariant), vehicleVariant ??
                        new()
                        {
                            Enabled = true,
                            VehicleModelId = VehicleModel.Id,
                            BodySpecs = new(),
                            TechnicalSpecs = new() { ConsumptionKmL = new() }
                        }
                    }
                });
            }
        }

        #endregion MODEL VARIANTS
    }
}
