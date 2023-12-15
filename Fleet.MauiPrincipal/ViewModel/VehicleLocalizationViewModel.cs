
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fleet.MauiPrincipal.Service;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Map = Microsoft.Maui.Controls.Maps.Map;


namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleLocalizationViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        [ObservableProperty]
        public double _longitude;
        [ObservableProperty]
        public double _latitude;
        [ObservableProperty]
        public string  _address;
        [ObservableProperty]
        public string _description;
  
        private Map _map;
        public Map Map
        {
            get { return _map; }
            set
            {
                if(Map != value)
                {
                    _map = value;
                    OnPropertyChanged(nameof(Map));
                }
            }
        }
      
        public VehicleLocalizationViewModel()
        {
           // vehicleParameter = vehicle;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehiclesAsync();
       
            //GetLocalizationMaps();
        }
        private LocationVehicele _locationVehicle;
        public LocationVehicele LocationVehicle
        {
            get { return _locationVehicle; }
            set
            {
                _locationVehicle = value;
                OnPropertyChanged(nameof(LocationVehicle));
            }
        }

        private Location _locat;
        public Location Locat
        {
            get { return _locat; }
            set
            {
                _locat = value;
                OnPropertyChanged(nameof(Locat));
            }
        }

        private Vehicle _selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                if (SelectedVehicle != value)
                {
                    _selectedVehicle = value;
                    OnPropertyChanged(nameof(SelectedVehicle));
                }

            }
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

        //Metodo para carregar a Vehicle e anexar no Picker
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

        private async Task GetVehicleLocation()
        {
            LocationVehicle = new LocationVehicele();
            var url = $"{baseUrl}/api/Location/GetLocationById/{_selectedVehicle.Id}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<LocationVehicele>
                        (responseStream, _SerializerOptions);
                    LocationVehicle = data;
                    Latitude = LocationVehicle.Lantitude;
                    Longitude = LocationVehicle.Longitude;
                    Locat = new Location(Latitude, Longitude);
                    Address = "Testando";
                    Description = "Testando descricao";

                    MapSpan mapSpan = new MapSpan(Locat, 0.01, 0.01);
                    Map = new Map(mapSpan);
                    
                    Map = new Map(MapSpan.FromCenterAndRadius(Locat, Distance.FromKilometers(1)));
                    //Map.MoveToRegion(MapSpan.FromCenterAndRadius(Locat, Distance.FromKilometers(1)));

                }
        }
        public ICommand GetLocalizationMapsCommand => new Command(async () =>
             await GetLocalizationMaps());
        private async Task GetLocalizationMaps()
        {
            GetVehicleLocation();
            //Latitude = LocationVehicle.Lantitude;
            //Longitude = LocationVehicle.Longitude;
            //Locat = new Location(Latitude, Longitude);
            //Debug.WriteLine("Entrou no get Location " + LocationVehicle.Lantitude);
            //Debug.WriteLine("Entrou no get Location latitude " + LocationVehicle.Lantitude);
            //Address = "Testando";
            //Description = "Testando descricao";
            ////Map = new Map(MapSpan.FromCenterAndRadius(Locat, Distance.FromKilometers(10)));
            ////Map.MoveToRegion(MapSpan.FromCenterAndRadius(Locat, Distance.FromKilometers(10)));

        }
    }
}
