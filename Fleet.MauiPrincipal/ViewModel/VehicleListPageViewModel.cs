using CommunityToolkit.Mvvm.ComponentModel;
using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleet.MauiPrincipal.View.Vehicle;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using System.Windows.Input;
using Fleet.MauiPrincipal.Service;
using System.Text.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleListPageViewModel : ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        public ObservableCollection<Vehicle> VehicleItems { get; } = new();
        [ObservableProperty]
        public string _filter;
        [ObservableProperty]
        public string _assignedState;

        public ObservableCollection<Vehicle> SelectedItems { get; set; } = new ObservableCollection<Vehicle>();
        public Vehicle SelectedVehicle { get; set; } = new Vehicle();
        [ObservableProperty]
        public ObservableCollection<Vehicle> _vehicleList;
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

        public VehicleListPageViewModel()
        {
            Client = new HttpClient();

            Vehicles = new List<Vehicle>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CarregarVehiclesAsync();
        }
        public VehicleListPageViewModel(string token)
        {
            Client = new HttpClient();

            Vehicles = new List<Vehicle>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            CarregarVehiclesAsync();
        }
        public ICommand CarregarVehiclesFilterCommand => new Command(async () =>
             await CarregarVehiclesFilterAsync());
        private async Task CarregarVehiclesFilterAsync()
        {
            VehicleList = new ObservableCollection<Vehicle>();
            var url = $"{baseUrl}/FleetTransport/Vehicle/GetVehicles/{Filter}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Vehicle>>
                        (responseStream, _SerializerOptions);

                    VehicleList = data;
                    Debug.WriteLine("Carregou filter");

                }
        }
        public ICommand VoltarCommand => new Command(async () =>
             await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public ICommand CarregarVehiclesCommand => new Command(async () =>
             await CarregarVehiclesAsync());
        private async Task CarregarVehiclesAsync()
        {
            VehicleList = new ObservableCollection<Vehicle>();
            var url = $"{baseUrl}/FleetTransport/Vehicle";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Vehicle>>
                        (responseStream, _SerializerOptions);

                    VehicleList = data;
    
                }
        }
 
        public ObservableCollection<Vehicle> Items { get; set; }

        public ICommand GoToAddVehicleCommand => new Command(async () =>
             await GoToAddVehicleAsync());
        private async Task GoToAddVehicleAsync()
        {
            Vehicle v = new Vehicle();
            await Application.Current.MainPage.Navigation.PushAsync(new VehicleAddPage(v));
        }

        [RelayCommand]
        public async void DisplayAlert(Vehicle vehicle)
        {
            Vehicle VehicleSeleted = new Vehicle() ;
            if (VehicleList != null && VehicleList.Contains(vehicle))
            {   VehicleSeleted = vehicle;
                var option = await Application.Current.MainPage.DisplayActionSheet("Selecionar a Opção", "Ok", null, "Atualizar", "Detalhes");
                if (option == "Atualizar")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new VehicleAddPage(VehicleSeleted));
                }
                else if (option == "Detalhes")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetailPage(VehicleSeleted.Id));
                }
            }

        }
    
    }
}
