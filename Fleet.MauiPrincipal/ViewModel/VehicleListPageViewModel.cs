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

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleListPageViewModel : ObservableObject , INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        //[ObservableProperty]
        //public String _IdProcurar;
        //[ObservableProperty]
        //public String _SbProcurar;
      
        //[ObservableProperty]
        //public ObservableCollection<Vehicle> _vehicles;

        //private Vehicle selectedEmployee;
        //public Vehicle SelectedEmployee
        //{
        //    get { return selectedEmployee; }
        //    set
        //    {
        //        if (SelectedEmployee != value)
        //        {
        //            selectedEmployee = value;
        //            OnPropertyChanged(nameof(SelectedEmployee));
        //        }

        //    }
        //}


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

        public VehicleListPageViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehiclesAsync();

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

        [ICommand]
        public async void CarregarVehicleID ()
        {
            //var vehicleID = Convert.ToInt32(SbProcurar);
            //if (!string.IsNullOrEmpty(SbProcurar))
            //{
                //if (vehicleID > 0)
                //{
                //    var url = $"{baseUrl}/FleetTransport/Vehicle/{SbProcurar}";
                //    var response = await Client.GetAsync(url);
                //    if (response.IsSuccessStatusCode)
                //    {
                //        using (var responseStream = await response.Content.ReadAsStreamAsync())
                //        {
                //            var data = await JsonSerializer.DeserializeAsync<Vehicle>(responseStream, _SerializerOptions);
                //            Vehicle = data;
                //        }
            //    //    }
            //    await AppShell.Current.DisplayAlert("ATT", "Quer procurar pelo ID do Veículo", "OK");
            //    //}
            //}
            //else
            //{
             
                await Shell.Current.GoToAsync(nameof(VehicleBrandPage));
            //}
            
        }


        //Metodo do searchbar

        //public async void CarregarVehicleID ()
        //{
        //    await Shell.Current.DisplayAlert("Alerta","Pretendes Fazer Procura ? ","OK"); 


        //}




        [ICommand]
        public async void DisplayAction(Employee employee)
        { 
            //var url = $"{baseUrl}/FleetTransport/Vehicle/{SbProcurar}";
            var option = await AppShell.Current.DisplayActionSheet("Select Option", " OK ", null, "Edit", "Delete");

            //if (option == "Edit")
            //{
            //    await AppShell.Current.GoToAsync(nameof(VehicleAddPage));
          
            //}
            //if (option == "Delete")
            //{
            //    var response = await Client.DeleteAsync(url);
              
            //    if (response.IsSuccessStatusCode )
            //    {
            //        await  CarregarVehiclesAsync();
            //    }
            //}
        }

    }
}
