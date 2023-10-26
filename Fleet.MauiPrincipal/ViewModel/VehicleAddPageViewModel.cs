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
        public int _vehicleTipo;
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
      
        public VehicleAddPageViewModel()
        {

            Client = new HttpClient();
            Vehicles = new ObservableCollection<Vehicle>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehicleMarcaAsync();

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



        public ICommand CadastraVehicleCommand => new Command(async () =>
      await CadastraVehicleAsync());

        public bool isNewItem = false;

        private async Task CadastraVehicleAsync()
        {
            var url = $"{baseUrl}/FleetTransport/Vehicle";
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
                    Registration = Registro,
                    Transmission = TransmitionType,
                    FuelConsumption = FuelConsumption,
                    Type = VehicleTipo,
                    YearOfManufacture = DataFabrico,
                    RegistrationDate = DateTime.Today
                };
                Debug.WriteLine("Testando enum o valo do type é" + VehicleTipo);
                Debug.WriteLine("Testando  o valo do transmission é é" + TransmitionType);  
                Debug.WriteLine("Testando  o valo do transmission é é" + _selectedMarca.Name);

                string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                //HttpResponseMessage response = null; 
                //if (isNewItem)
                //{      response = await Client.PostAsync(url, content);
                //    await AppShell.Current.DisplayAlert("Informação", "Veiculo Cadastrado com sucesso!", "Ok");
                //}
                //else{   
                //      response = await Client.PutAsync(url, content);
                //     await AppShell.Current.DisplayAlert("Informação", "Veiculo atualizado com sucesso!" , "Ok");
                //}
                ////if (!response.IsSuccessStatusCode)
                ////{
                ////    await AppShell.Current.DisplayAlert("Informação", "A operação falhou!", "Ok");
                ////}
            }
            else
            { Debug.WriteLine(" Campos Obrigatorios vazio!"); }
              //await Shell.Current.GoToAsync(nameof(VehicleListPage));
              await Application.Current.MainPage.Navigation.PopAsync();
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
