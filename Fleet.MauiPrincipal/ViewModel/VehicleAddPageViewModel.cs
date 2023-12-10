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
        public string _transmitionType;
        [ObservableProperty]
        public string _registration;
        [ObservableProperty]
        public string _cor;
        [ObservableProperty]
        public string _vin;
        [ObservableProperty]
        public string _vehicleTipo;
        [ObservableProperty]
        public int _power;
        [ObservableProperty]
        public double _fuelConsumption;
        [ObservableProperty]
        public string _variante;
        [ObservableProperty]
        public Vehicle _vehicle;
      
        public Vehicle Vehicles;
        [ObservableProperty]
        public string _model;
        [ObservableProperty]
        public int _dataFabrico; 
        [ObservableProperty]
        public string _assignedState;
        [ObservableProperty]
        public string _marca;
        public Vehicle vehicleUpdate { get; set; } 
        public VehicleAddPageViewModel(Vehicle vehicle)
        {
            vehicleUpdate = vehicle;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            FillTheFields();
            CarregarVehicleMarcaAsync();
            //CarregarVehiclesAsync();

        }   
        // Metodo Carregar Vehicle Type
        private string _vehicleType;
        public List<string> VehicleTypes
        {
            get
            {
                //return Enum.GetNames(typeof(VehicleType)).Select(b => b).ToList();
                return new List<String> {  "Carro", "Motorizada", "Camião", "Autocarro" };
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
                    
                }
            }      
    }
        // Metodo Carregar Vehicle Type
        private string _vehicleTransmission;
      public List<string> VehicleTransmission
        {
            get
            {             //return  Enum.GetNames(typeof(TransmissionType)).Select(b => b).ToList(); 
                return new List<string> {  "Manual", "Automatico", "CVT", "AMT" };
                
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
           
                }
             
            }

        }

        // Metodo Carregar cores
        private string _colors;
        public List<string> Colors
        {
            get
            {             //return  Enum.GetNames(typeof(TransmissionType)).Select(b => b).ToList(); 
                return new List<string> {  "Branco", "Preto", "Prata", "Azul","Vermelho","Verde","Amarelo", "Cinza", "Marrom", "Laranja" };

            }
        }
        private string _selectedColor;

        public string SelectedColors
        {
            get { return _selectedColor; }
            set
            {
                if (SelectedColors != value)
                {
                    _selectedColor = value;
                    OnPropertyChanged(nameof(SelectedColors));

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
        private VehicleBrand _selectedMarca ;
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
    
        public ICommand GoToVehicleAddSecondCommand => new Command(async () =>
            await GoToVehicleAddSecondAsync());
        private async Task GoToVehicleAddSecondAsync()
        {
                await Application.Current.MainPage.Navigation.PushAsync(new NewPage2
                             (vehicleUpdate.Id, Vin, _selectedColor, _selectedTransmission, _selectedVehicleType, _selectedMarca.Name, Registration));
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
            TransmitionType = "";
            FuelConsumption = 0;
            VehicleTipo = "";
            DataFabrico = 0;
        }   
        public void FillTheFields()
        {
            Vin = vehicleUpdate.Vin;
            Registration = vehicleUpdate.Registration;
            Cor = vehicleUpdate.Cor;
            Brand = vehicleUpdate.Brand;
            VehicleTipo = vehicleUpdate.Type;
            TransmitionType = vehicleUpdate.Transmission;

        }
        public ICommand VoltarCommand => new Command(async () =>
            await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
