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
    public partial class VehicleAddSecondViewModel : ObservableObject
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
        public List<Vehicle> Vehicles;
        [ObservableProperty]
        public string _model;
        [ObservableProperty]
        public int _dataFabrico;
        [ObservableProperty]
        public string _assignedState;
        [ObservableProperty]
        public string _marca;
        public bool isNewItem;
        string path;
        int Id;
        public Vehicle vehicleUpdate { get; set; }
        public VehicleAddSecondViewModel(int id, string vin, string cor, string transmission, string typeVehicle, string brand, string registration)
        {
            Id = id;  Vin = vin; Cor = cor; TransmitionType = transmission; VehicleTipo = typeVehicle; Brand = brand; Registration = registration;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
           // FillTheFields();
            CarregarVehiclesAsync();
        }
         public VehicleAddSecondViewModel(Vehicle vehicle)
           {
            vehicleUpdate = vehicle;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
           // FillTheFields();
            CarregarVehiclesAsync();
        }

        public void VerifyNewVehicles()
        {
                if (Id == 0)
                {
                    isNewItem = true;
                    path = "/FleetTransport/Vehicle";
                }
                else if(Id>0)
                {
                    isNewItem = false;
                    path = $"/FleetTransport/Vehicle/{Id}";
                   
                }
        }
        public ICommand VoltarCommand => new Command(async () =>
                    await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        
        public ICommand CadastraVehicleCommand => new Command(async () =>
      await CadastraVehicleAsync());
        private async Task CadastraVehicleAsync()
        {
            VerifyNewVehicles();
            var url = $"{baseUrl + path}";
            Debug.WriteLine("O caminho é: ", url);
     
                var vehicle = new Vehicle
                {
                    Vin = Vin,   Cor = Cor, Variant = Variante, Brand = Brand,  Model = Model,Power = Power,Registration = Registration,
                     Transmission = TransmitionType,  FuelConsumption = FuelConsumption, Type = VehicleTipo,  YearOfManufacture = DataFabrico, 
                      RegistrationDate = DateTime.Today,
                };
                string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (isNewItem.Equals(false))
                {
                    response = await Client.PutAsync(url, content);
                 
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Informação", "Atualizado com sucesso!", "Ok");
                        CleanFields();
                        await Application.Current.MainPage.Navigation.PushAsync(new VehicleListPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Informação", "Não Atualizado !", "Ok");
                    }
                }
                else
                { 
                    response = await Client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Informação", "Cadastrado com sucesso!", "Ok");
                        CleanFields();
                        await Application.Current.MainPage.Navigation.PushAsync(new VehicleListPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Informação", "Não Cadastrado !", "Ok");
                    }
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

        public ICommand GoTOVehicleAddPageCommand => new Command(async () =>
                await GoTOVehicleAddPageAsync());
        private async Task GoTOVehicleAddPageAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public void CleanFields()
        {
            Variante = "";
            Model = "";
            Power = 0;
            TransmitionType = "";
            FuelConsumption = 0;
            DataFabrico = 1900;
        }
        public void FillTheFields()
        {
            Model = vehicleUpdate.Model;
            Variante = vehicleUpdate.Variant;
            DataFabrico = vehicleUpdate.YearOfManufacture;
            Power = vehicleUpdate.Power;
            FuelConsumption = vehicleUpdate.FuelConsumption;
        }
    }
}
