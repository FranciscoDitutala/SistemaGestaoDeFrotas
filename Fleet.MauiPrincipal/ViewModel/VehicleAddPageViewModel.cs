using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.Service.Enums;
using Fleet.MauiPrincipal.View.Vehicle;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleAddPageViewModel : ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        [ObservableProperty]
        public string _brand;
        [ObservableProperty]
        public int _transmitionType;
        [ObservableProperty]
        public string _registration;
        [ObservableProperty]
        public string _cor;
        [ObservableProperty]
        public string _vin;
        [ObservableProperty]
        public int _vehicleTipo;
        [ObservableProperty]
        public int _power;
        [ObservableProperty]
        public double _fuelConsumption;
        [ObservableProperty]
        public string _variante;
        [ObservableProperty]
        public Vehicle _vehicle;
      
        public List<Vehicle> Vehicles;
        [ObservableProperty]
        public string _model;
        [ObservableProperty]
        public int _dataFabrico;
        public Vehicle vehicleUpdate { get; set; } 
        public VehicleAddPageViewModel(Vehicle vehicle)
        {
            vehicleUpdate = vehicle;
            Client = new HttpClient();
            //Vehicles = new ObservableCollection<Vehicle>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            FillTheFields();
            CarregarVehicleMarcaAsync();
            CarregarVehiclesAsync();

        }   
        // Metodo Carregar Vehicle Type
        private string _vehicleType;

        public List<string> VehicleTypes
        {
            get
            {
                //return Enum.GetNames(typeof(VehicleType)).Select(b => b).ToList();
                return new List<String> { "Nenhum", "Carro", "Motorizada", "Camião", "Autocarro" };
            }
        }

        private string _selectedVehicleType;
        public string SelectedVehicleTypes
        {
            get {  return _selectedVehicleType; }
            set
            {
                if (SelectedVehicleTypes != value)
                {
                    _selectedVehicleType = value;
                    OnPropertyChanged(nameof(SelectedVehicleTypes));
                    if (_selectedVehicleType.Equals("Carro"))
                    {
                        VehicleTipo = 1;
                    }
                    else if (_selectedVehicleType.Equals("Motorizada"))
                    {
                        VehicleTipo = 2;
                    }
                    else if (_selectedVehicleType.Equals("Camião"))
                    {
                        VehicleTipo = 3;
                    }
                    else if (_selectedVehicleType.Equals("Autocarro"))
                    {
                        VehicleTipo = 4;
                    }
                    else { VehicleTipo = 0; }
                }
            }      
    }
        // Metodo Carregar Vehicle Type
        private string _vehicleTransmission;
      public List<string> VehicleTransmission
        {
            get
            {
                //return  Enum.GetNames(typeof(TransmissionType)).Select(b => b).ToList(); 
                return new List<string> { "Nenhum", "Manual", "Automatico", "CVT", "AMT" };
                
            }
        }
        private string _selectedTransmission;
        public string SelectedTransmissions
        {
            get { return _selectedTransmission; }
            set
            {
                if (SelectedTransmissions != value)
                {
                    _selectedTransmission = value;
                    OnPropertyChanged(nameof(SelectedTransmissions));
                    if (_selectedTransmission.Equals("Manual"))
                    {
                        TransmitionType = 1;
                    }
                    else if (_selectedTransmission.Equals("Automatico"))
                    {
                        TransmitionType = 2;
                    }
                    else if (_selectedTransmission.Equals("CVT"))
                    {
                        TransmitionType = 3;
                    }
                    else if (_selectedTransmission.Equals("AMT"))
                    {
                        TransmitionType = 4;
                    }
                    else { TransmitionType = 0; }
                }
            }

        }
        // Metodo Carregar Vehicle Marca  
        private ObservableCollection<VehicleBrand> _vehicleMarca;
        public ObservableCollection<VehicleBrand> VehicleMarcas
        {
            get { return _vehicleMarca; }
            set
            {
                _vehicleMarca = value;
                OnPropertyChanged(nameof(VehicleMarcas));
            }
        }
        private VehicleBrand _selectedMarca;
        public VehicleBrand SelectedMarcas
        {
            get { return _selectedMarca; }
            set
            {
                if (SelectedMarcas != value)
                {
                    _selectedMarca = value;
                    OnPropertyChanged(nameof(SelectedMarcas));
                }
            }
        }
        private async Task CarregarVehicleMarcaAsync()
        {
            VehicleMarcas = new ObservableCollection<VehicleBrand>();
            var url = $"{baseUrl}/FleetCommon/VehicleBrand";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<VehicleBrand>>
                        (responseStream, _SerializerOptions);

                    VehicleMarcas = data;
                }
        }

        public bool isNewItem ;
        string path;
        int paramId;
        public void VerifyNewVehicles()
        {
            foreach (var item in Vehicles)
            {
                if (!item.Vin.Equals(Vin) && !item.Registration.Equals(Registration))
                {
                    isNewItem = true;
                    path = "/FleetTransport/Vehicle";
                }
                else
                {
                    isNewItem = false;
                    path = $"/FleetTransport/Vehicle/{item.Id}";
                    paramId = item.Id;
                }
            }
        }
        public ICommand CadastraVehicleCommand => new Command(async () =>
      await CadastraVehicleAsync());

        private async Task CadastraVehicleAsync()
        {
            VerifyNewVehicles();
            var url = $"{baseUrl + path}";
            if (!string.IsNullOrEmpty(Vin))
            {
                var vehicle = new Vehicle
                {
                    Vin = Vin,
                    Cor = Cor,
                    Variant = Variante,
                    Brand = _selectedMarca.Name,
                    Model = Model,
                    Power = Power,
                    Registration = Registration,
                    Transmission = TransmitionType,
                    FuelConsumption = FuelConsumption,
                    Type = VehicleTipo,
                    YearOfManufacture = DataFabrico,
                    RegistrationDate = DateTime.Today,
                    //Assigned = false;
                };
                string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                Debug.WriteLine("o valor do json é " + json);
                HttpResponseMessage response = null;
                if (isNewItem.Equals(false)){ 
                        response = await Client.PutAsync(url, content);
                        await Application.Current.MainPage.DisplayAlert("Informação ", "Veiculo Atualizado com sucesso!", "Ok");
                        CleanFields();
                        await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetailPage(paramId));
                    }
                    else
                    {
                        response = await Client.PostAsync(url, content);
                        await Application.Current.MainPage.DisplayAlert("Informação", "Veiculo Cadastrado com sucesso!", "Ok");
                        CleanFields();
                        await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Falhou ", "Veiculo nao foi cadastrado nem atualizado!", "Ok");
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
        public void CleanFields()
        {
            Vin = "";
            Cor = "";
            Variante = "";
            Brand = "";
            Model = "";
            Power = 0;
            Registration = "";
            TransmitionType = 0;
            FuelConsumption = 0;
            VehicleTipo = 0;
            DataFabrico = 0;
        }   
        public void FillTheFields()
        {
            Model = vehicleUpdate.Model;
            Variante = vehicleUpdate.Variant;
            DataFabrico = vehicleUpdate.YearOfManufacture;
            Power = vehicleUpdate.Power;
            Vin = vehicleUpdate.Vin;
            Registration = vehicleUpdate.Registration;
            Cor = vehicleUpdate.Cor;
            FuelConsumption = vehicleUpdate.FuelConsumption;
        }
       
    }
}
