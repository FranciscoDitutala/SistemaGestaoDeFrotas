using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.View.Vehicle;

using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    [QueryProperty(nameof(VehicleAddPage), "AddVehicle")]

    public partial class VehicleAddPageViewModel : ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        //[ObservableProperty]
        //private Vehicle _addVehicle = new Vehicle();
        //private readonly IVehicleService _vehicleService;

        [ObservableProperty]
        public string _Brand;
        [ObservableProperty]
        public int _TransmitionType;
        [ObservableProperty]
        public string _Registro;
        [ObservableProperty]
        public string _Cor;
        [ObservableProperty]
        public string _Vin;
        [ObservableProperty]
        public int _VehicleType;
        [ObservableProperty]
        public int _Power;
        [ObservableProperty]
        public int _FuelConsumption;
        [ObservableProperty]
        public string _Variante;
        [ObservableProperty]
        public Vehicle _Vehicle;
        [ObservableProperty]
        public ObservableCollection<Vehicle> _Vehicles;
        [ObservableProperty]
        public string _Model;
        [ObservableProperty]
        public int _DataFabrico;
       

        //[ObservableProperty]
        //public DateTime _DataRegistro;


        public VehicleAddPageViewModel()
        {

            Client = new HttpClient();
            Vehicles = new ObservableCollection<Vehicle>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CadastraVehicleAsync();

        }


        //public ICommand CarregarVehiclesCommand => new Command(async () =>
        //await CarregarVehiclesAsync());
        //private async Task CarregarVehiclesAsync()
        //{
        public ICommand CadastraVehicleCommand => new Command(async () =>
        await CadastraVehicleAsync());

        private async Task CadastraVehicleAsync()
        {
            if (!string.IsNullOrEmpty(Vin))
            {
                var vehicle = new Vehicle
                {
                    Vin = Vin,
                    Cor = Cor,
                    Variant = Variante,
                    Brand = Brand,
                    Model = Model,
                    Power = Power,
                    Registration = "" + Registro,
                    Transmission = TransmitionType,
                    FuelConsumption = FuelConsumption,
                    Type = VehicleType,
                    YearOfManufacture = DataFabrico,
                    RegistrationDate =DateTime.Today
                    
                };
                var url = $"{baseUrl}/FleetTransport/Vehicle";
                Debug.WriteLine("a COR DO VEICULO É " + vehicle.Cor);
                string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                ////await AppShell.Current.DisplayAlert("Cadastrado com sucesso","O veiculo foi adicionado no sistema ","OK");
                Debug.WriteLine("o veiculofoi cadastrado " + json);
                //await CarregarVehiclesAsync();
            }
              Debug.WriteLine("Não entrou no if");
        }
       

   


        //public ICommand CarregarVehiclesCommand => new Command(async () =>
        //await CarregarVehiclesAsync());
        //private async Task CarregarVehiclesAsync()
        //{
        //    //var url= $"{baseUrl}FleetTransport/Vehicle";

        //    var response = await Client.GetAsync(Client.BaseAddress);
        //    Console.WriteLine("dados do data" + response);
        //    if (response.IsSuccessStatusCode)
        //         using( var responseStream = await response.Content.ReadAsStreamAsync())
        //        {
        //            Console.WriteLine("dados do response" + response);
        //            var data = await JsonSerializer.DeserializeAsync<ObservableCollection<Vehicle>>
        //                (responseStream, _SerializerOptions);
        //            Console.WriteLine("dados do data" + data);
        //            Vehicles = data;
        //        }
        //}

        //   public ICommand CarregarVehicleIDCommand =>
        //       new Command(async () =>
        //   {
        //   var vehicleID = Convert.ToInt32(VID);
        //   if (vehicleID > 0) {
        //       var url = $"{baseUrl}/FleetTransport/Vehicle/{VID}";
        //       var response = await Client.GetAsync(url);
        //       if (response.IsSuccessStatusCode)
        //       {
        //           using (var responseStream = await response.Content.ReadAsStreamAsync())
        //       {
        //                   var data = await JsonSerializer.DeserializeAsync<Vehicle>(responseStream, _SerializerOptions);
        //                   Vehicle = data;
        //       }
        //           }
        //          } 
        //   });

        //   public ICommand SalvarVehicleCommand => new Command(async () =>
        //   {
        //       var url = $"{baseUrl}/FleetTransport/Vehicle";
        //       if(VID > 0)
        //       {
        //           var vehicle = new Vehicle
        //           {
        //               Id = VID,
        //               BrandId = Marca,
        //               ModelId = Modelo,
        //               Power = Power,
        //               TransmissionType = TransmitionType,
        //               FuelConsumption = FuelConsumption,
        //               VariantId = Variante,
        //               TypeVehicle = TypeVehicle,
        //               YearOfManufacture = DataFabrico,
        //               RegistrationDate = DataRegistro
        //           };
        //           string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
        //           StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //           var response = await Client.PostAsync(url, content);
        //           await CarregarVehiclesAsync();
        //       }
        //   }
        //   );

        //   public ICommand UpdateVehicleCommand => new Command(async () =>
        //   {

        //       if ((VID > 0) && (Marca > 0) & (Modelo>0) && (TransmitionType>0) && (Variante>0))
        //       {
        //           var vehicle = Vehicles.FirstOrDefault(x => x.Id == VID);
        //           var url = $"{baseUrl}/FleetTransport/Vehicle/{VID}";

        //           vehicle.Id = VID;
        //           vehicle.BrandId = Marca;
        //           vehicle.ModelId = Modelo;
        //           vehicle.Power = Power;
        //           vehicle.TransmissionType = TransmitionType;
        //           vehicle.FuelConsumption = FuelConsumption;
        //           vehicle.VariantId = Variante;
        //           vehicle.TypeVehicle = TypeVehicle;
        //           vehicle.YearOfManufacture = DataFabrico;
        //           vehicle.RegistrationDate = DataRegistro;

        //           string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
        //           StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //           var response = await Client.PutAsync(url, content);
        //           await CarregarVehiclesAsync();
        //       }
        //   }
        //);




    }
}
