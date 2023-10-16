using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    
    public partial class VehicleModelPageViewModel : ObservableObject, INotifyPropertyChanged
    {
    private HttpClient Client;
    JsonSerializerOptions _SerializerOptions;
    string baseUrl = "https://localhost:7111";
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
        static Random random = new();
        [ObservableProperty]
        public string _nameModel;
        [ObservableProperty]
        public string _acronymModel;
        [ObservableProperty]
        public int _brandModel;
        public ObservableCollection<VehicleModel> ModelItems { get; } = new();

        //public VehicleModel Modelo { get; set; } = new();
        private VehicleModel _models;

        public VehicleModel Models 
        {
            get { return _models; }
            set
            {
                _models = value;
                OnPropertyChanged(nameof(Models));
            }
        }

        public VehicleModelPageViewModel()
        {
            
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarModelsAsync();
        }

        public ICommand CarregarModelsCommand => new Command(async () =>
           await CarregarModelsAsync());
        private async Task CarregarModelsAsync()
        {
            Debug.WriteLine("Entrou no metodo carregar modelo");

            Models = new VehicleModel();
            var url = $"{baseUrl}/FleetCommon/VehicleModel/2";
            var response = await Client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<VehicleModel>
                        (responseStream, _SerializerOptions);
                    Models = data;
                    Debug.WriteLine("As informacoes da lista " + Models);
                }
        }

        public ICommand CadastraVehicleModelCommand => new Command(async () =>
            await CadastraVehicleModelAsync());

        private async Task CadastraVehicleModelAsync()
        {
            if (!string.IsNullOrEmpty(NameModel))
            {
                var model = new VehicleModel
                {
                    Name = NameModel,
                    Acronym = AcronymModel,
                    VehicleBrandId = BrandModel,
                };
                var url = $"{baseUrl}/FleetCommon/VehicleModel";
                string json = JsonSerializer.Serialize<VehicleModel>(model, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                Debug.WriteLine("o veiculo Modelo foi cadastrado " + json);
                await CarregarModelsAsync();
            }
        }



    }
}
