using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        [ObservableProperty]
        public int _modelName;
        [ObservableProperty]
        public string _arcronymModel;



        static Random random = new();
        public ObservableCollection<VehicleModel> ModelItems { get; } = new();
        private VehicleModel _modelo;
        public VehicleModel Modelo { get; set; } = new();
        private List<VehicleModel> _models;

        public List<VehicleModel> Models 
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
                if (ModelName > 0)
                {
                    //var modelId = Convert.ToInt32(ModelName);
                    if (ModelName > 0)
                    {
                        var url = $"{baseUrl}/FleetCommon/VehicleModel/1";
                        var response = await Client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            using (var responseStream = await response.Content.ReadAsStreamAsync())
                            {
                                var data = await JsonSerializer
                                .DeserializeAsync<VehicleModel>(responseStream, _SerializerOptions);
                                Modelo = data;
                            }

                        }
                    }


                }
            }  
       }
}
