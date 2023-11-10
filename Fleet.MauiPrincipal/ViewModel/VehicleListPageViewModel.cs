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

        private List<Vehicle> _vehicles;
        public List<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value; 
                OnPropertyChanged(nameof(Vehicles));
            }
        } private List<Vehicle> _vehiclesFilter;
        public List<Vehicle> VehiclesFilter
        {
            get { return _vehiclesFilter; ; }
            set
            {
                _vehiclesFilter = value; 
                OnPropertyChanged(nameof(VehiclesFilter));
            }
        }

        public VehicleListPageViewModel(string token)
        {
            Client = new HttpClient();
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
              
          
            CarregarVehiclesAsync();
        }
        public ICommand CarregarVehiclesFilterCommand => new Command(async () =>
             await CarregarVehiclesFilterAsync());
        private async Task CarregarVehiclesFilterAsync()
        {
            Vehicles = new List<Vehicle>();
            var url = $"{baseUrl}/FleetTransport/Vehicle/GetVehicles/{Filter}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Vehicle>>
                        (responseStream, _SerializerOptions);

                    Vehicles = data;
                    Debug.WriteLine("Carregou filter");

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
                    VerifyAssigned();
                }
        }
        public void VerifyAssigned()
        {
            //foreach (var item in Vehicles)
            //{
            //    if (item.Assigned.Equals(false))
            //    {
            //        item.Assigned = (string)"Não Atribuido";
            //    }
            //    else { AssignedState = "Atribuido"; }
            //}
        }
        public ObservableCollection<Vehicle> Items { get; set; }
        public ObservableCollection<Vehicle> SelectedItems { get; set; } = new ObservableCollection<Vehicle>();

        public ICommand GoToVehicleDetalhesCommand => new Command(async () =>
             await GoToVehicleDetalhesAsync());
        public async Task GoToVehicleDetalhesAsync()
        {
            Items = new ObservableCollection<Vehicle>(_vehicles);
            foreach (var item in SelectedItems)
            {
                if (Items.Contains(item))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetailPage(item.Id));
                }
            } 
        }
        public ICommand GoToAddVehicleCommand => new Command(async () =>
             await GoToAddVehicleAsync());
        private async Task GoToAddVehicleAsync()
        {
            Vehicle v = new Vehicle();
            await Application.Current.MainPage.Navigation.PushAsync(new VehicleAddPage(v));
        }
    }
}
