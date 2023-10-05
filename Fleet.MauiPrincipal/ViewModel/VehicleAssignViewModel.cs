using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.ViewModel
{
    public  class VehicleAssignViewModel: ObservableObject, INotifyPropertyChanged
    {
        //[ObservableProperty]
        //public ObservableCollection _Items { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        private Vehicle selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set {
              if(SelectedVehicle != value)
                {
                    selectedVehicle = value;
                    OnPropertyChanged(nameof(SelectedVehicle));
                }
                 
            }
        }
       
        private List<Vehicle> _vehicles;
        public List<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set { _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }
         public VehicleAssignViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehiclesAsync();
            CarregarEmployee();
            //Vehicles = new Vehicle { }

        }

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

            //Vehicles = new List<Vehicle>
            //    { new Vehicle {
            //        BrandId = 1,
            //        ModelId = 1,
            //        VariantId = 1,
            //        YearOfManufacture = 2020,
            //        Type = 1,
            //        Power = 1,
            //        FuelConsumption = 1,
            //        Transmission = 0,
            //        RegistrationDate = DateTime.Today
            //    }};

        }
        // Metodo Carregar Empregado
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (SelectedEmployee != value)
                {
                    selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }

            }
        }

        private List<Employee> _employees;
        public List<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        private async Task CarregarEmployee()
        {
            Employees = new List<Employee>();
            var url = $"{baseUrl}/FleetTransport/Employee";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Employee>>
                        (responseStream, _SerializerOptions);

                    Employees = data;
                }

        
        }
    }
}
