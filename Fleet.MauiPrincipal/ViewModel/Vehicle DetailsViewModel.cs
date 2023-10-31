using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.View.Vehicle;
using Fleet.MauiPrincipal.Service;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    //[QueryProperty(nameof(VehicleListPage), "ItemsSelected")]
    public partial class Vehicle_DetailsViewModel : ObservableObject
    {

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
   
        [ObservableProperty]
        public string _matricula;

        public int vehicleId;
        VehicleListPageViewModel teste = new VehicleListPageViewModel();
        public Vehicle_DetailsViewModel(int Id)
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            vehicleId = Id;
            CarregarVehiclesAsync();

        }

        public ObservableCollection<Vehicle> Items { get; set; }
        public ObservableCollection<Vehicle> SelectedItems { get; set; } = new ObservableCollection<Vehicle>();

        private Vehicle _vehicles;
        public Vehicle Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        public ICommand CarregarVehiclesCommand => new Command(async () =>
         await CarregarVehiclesAsync());
        private async Task CarregarVehiclesAsync()
        {
            Vehicles = new Vehicle();
            var url = $"{baseUrl}/FleetTransport/Vehicle/GetVehicleDetail/{vehicleId}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<Vehicle>
                        (responseStream, _SerializerOptions);

                    Vehicles = data;
                    Debug.WriteLine("A matricula do veiculo é " + Vehicles.Registration);

                }
        }

        public ICommand GoToUpdateVehicleCommand => new Command(async () =>
             await GoToUpdateVehicleAsync());
        private async Task GoToUpdateVehicleAsync()
        {
          await Application.Current.MainPage.Navigation.PushAsync(new VehicleAddPage(Vehicles));
        }  
        
        public ICommand GoToAssignVehicleCommand => new Command(async () =>
             await GoToAssignVehicleAsync());
        private async Task GoToAssignVehicleAsync()
        {
          await Application.Current.MainPage.Navigation.PushAsync(new VehicleAssignPage(Vehicles));
        }



        public ICommand RemoveSelectedCommand => new Command(async () =>
             await DeletarVehiclesAsync());
        private async Task DeletarVehiclesAsync()
        {
           var option =  await Application.Current.MainPage.DisplayActionSheet("Pretende Apagar o veiculo? ", "Cancelar,", "Deletar");
            if(option.Equals("Deletar"))
            {
                var url = $"{baseUrl}/FleetTransport/Vehicle/{vehicleId}";
                var response = await Client.DeleteAsync(url);
                Debug.WriteLine("O veiculo foi apagado com id " + vehicleId);
                await Application.Current.MainPage.DisplayAlert("Atencao ","O veiculo deletado com sucesso ", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }  
            
        }
    }

}
