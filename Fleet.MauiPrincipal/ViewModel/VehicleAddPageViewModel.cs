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
        public int _power;
        [ObservableProperty]
        public double _fuelConsumption;
        [ObservableProperty]
        public string _variante;
        [ObservableProperty]
        public Vehicle _Vehicle;
        [ObservableProperty]
        public ObservableCollection<Vehicle> _Vehicles;
        [ObservableProperty]
        public string _model;
        [ObservableProperty]
        public int _dataFabrico;
      
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

        //public bool isNewItem = false;

        private async Task CadastraVehicleAsync()
        {
           
            if (!string.IsNullOrEmpty(Vin) )
            {
                Debug.WriteLine("Dentro do Vehicle");
                var vehicle = new Vehicle
                {
                    Vin = Vin,
                    Cor = Cor,
                    Variant = Variante,
                    Brand = _selectedMarca.Name,
                    Model = Model,
                    Power =  Power,
                    Registration = Registro,
                    Transmission = TransmitionType,
                    FuelConsumption = FuelConsumption,
                    Type = VehicleTipo,
                    YearOfManufacture = DataFabrico,
                    RegistrationDate = DateTime.Today
                };
                //Debug.WriteLine("Testando enum o valo do Variante é " + vehicle.Vin);
                //Debug.WriteLine("Testando enum o valo do type é " + vehicle.Type);
                //Debug.WriteLine("Testando  o valo do transmission é é " + vehicle.Transmission);  
                //Debug.WriteLine("Testando  o valo do Marca é é " + vehicle.Brand);
                //Debug.WriteLine("Testando enum o valo do Modelo é" + vehicle.Model);
                //Debug.WriteLine("Testando  o valo do Power é é " + vehicle.Power);
                //Debug.WriteLine("Testando  o valo do Consumo é é " + vehicle.FuelConsumption);
                //Debug.WriteLine("Testando enum o valo do Cor é " + vehicle.Cor);
                //Debug.WriteLine("Testando  o valo do Data é " +vehicle.YearOfManufacture);
                //Debug.WriteLine("Testando  o valo do Chassis é " + vehicle.RegistrationDate);
                //Debug.WriteLine("O chassis do Veiculo é " + vehicle.Vin);

                var url = $"{baseUrl}/FleetTransport/Vehicle";
                string json = JsonSerializer.Serialize<Vehicle>(vehicle, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                if (response.IsSuccessStatusCode) {
                    Debug.WriteLine("Cadastrado com sucesso" + vehicle.Id);
                    await Shell.Current.DisplayAlert("Ola", "O veiculo Foi Cadastrado com sucesso", "ok");
                }
                else
                {
                    Debug.WriteLine("Nao cadastrado o vehicle" + vehicle.Registration);
                    await Shell.Current.DisplayAlert("Ola", "O veiculo nao Foi Cadastrado", "ok");
                }
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
            //await Application.Current.MainPage.Navigation.PushAsync(new VehicleListPage());
         
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
