using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        public MainPageViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehiclesAsync();
            CarregarVehicleMarcaAsync();
            CarregarAssignmentAsync();
         }

        private List<Vehicle> _vehicles;
        public List<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }
        public ICommand CarregarVehiclesCommand => new Command(async () =>
         await CarregarVehiclesAsync());
        private async Task CarregarVehiclesAsync()
        {
            Vehicles = new List<Vehicle>();
            var url = $"{baseUrl}/FleetTransport/Vehicle";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Vehicle>>
                        (responseStream, _SerializerOptions);

                    Vehicles = data;
                }
        }
        private List<Assignment> _atribuicao;
        public List<Assignment> Atribuicoes
        {
            get { return _atribuicao; }
            set
            {
                _atribuicao = value;
                OnPropertyChanged(nameof(Atribuicoes));
            }
        }
        private async Task CarregarAssignmentAsync()
        {
            Atribuicoes = new List<Assignment>();
            var url = $"{baseUrl}/FleetTransport/Vehicle/GetAllVehiclesAssigned/{true}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Assignment>>
                        (responseStream, _SerializerOptions);
                    Atribuicoes = data;
                    //Debug.WriteLine("Carregado com sucesso a  atribuiçao " + Atribuicoes);

                }
            }
            // Metodo Carregar Vehicle Marca  
        private List<VehicleBrand> _vehicleMarca;
        public List<VehicleBrand> VehicleMarcas
        {
            get { return _vehicleMarca; }
            set
            {
                _vehicleMarca = value;
                OnPropertyChanged(nameof(VehicleMarcas));
            }
        }
        private async Task CarregarVehicleMarcaAsync()
        {
            VehicleMarcas = new List<VehicleBrand>();
            var url = $"{baseUrl}/FleetCommon/VehicleBrand";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<VehicleBrand>>
                        (responseStream, _SerializerOptions);

                    VehicleMarcas = data;
                }

        }
    }
   }
