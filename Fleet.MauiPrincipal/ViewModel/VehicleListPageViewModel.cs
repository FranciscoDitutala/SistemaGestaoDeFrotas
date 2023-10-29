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
using System.Diagnostics;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleListPageViewModel : ObservableObject 
    {
       
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        static Random random = new();
        public ObservableCollection<Vehicle> VehicleItems { get; } = new();

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
        public ObservableCollection<Vehicle> Items { get; set; }
        public ObservableCollection<Vehicle> SelectedItems { get; set; } = new ObservableCollection<Vehicle>();

        public Vehicle parametroVehicle { get; set; }
        //public Vehicle SelectedItems { get; set; } = new Vehicle();
        //public ICommand RemoveSelectedCommand { get; set; }

        public ICommand RemoveSelectedCommand => new Command(async () =>
             await DeletarVehiclesAsync());
        private async Task DeletarVehiclesAsync()
        {
               Items = new ObservableCollection<Vehicle>(_vehicles);
            foreach (var item in SelectedItems)
            {
                Debug.WriteLine("O TAMANHO DO selecteed items" + SelectedItems.Count);

                    //var teste = Items.Remove(item);
                    if (Items.Contains(item))
                 {
                    parametroVehicle = item;
                    var url = $"{baseUrl}/FleetTransport/Vehicle/{item.Id}";
                    var response = await Client.DeleteAsync(url);
                    Debug.WriteLine("A o veiculo foi apagado com id " + item.Id);
                }
                else
                {
                    Debug.WriteLine("veiculo nao foi apagado");
                }

                await CarregarVehiclesAsync();
            }
        }
        public ICommand GoToVehicleAddPageCommand => new Command(async () =>
             await GoToVehicleAddPageAsync());
        public async Task  GoToVehicleAddPageAsync()
        {
            //await Shell.Current.Go(new AppShell());
            //await Navigation.PushAsync(new AppShell());

            //var navParam = new Dictionary<string, object>();
            //navParam.Add("AddEmployee", employee);
            //await AppShell.Current.GoToAsync(nameof(AddEmployee), navParam);
            //GetEmployeesList();

        }



        [ICommand]
        public static async void UpdateVehicle()
        {
            await AppShell.Current.GoToAsync(nameof(VehicleDetailPage));
            //Debug.WriteLine("Entrou no sistema");


        }
        public ICommand GoToDetailsVehicleCommand => new Command(async () =>
        await GoToVehicleDeataisAsync());
        public async Task GoToVehicleDeataisAsync()
        {
            //await Shell.Current.Go(new AppShell());
            //await Navigation.PushAsync(new AppShell());
            Debug.WriteLine("Dentro do metodo veiculo details ");
            //Items = new ObservableCollection<Vehicle>(_vehicles);
            //foreach (var item in SelectedItems)
            //{
            //    if (Items.Contains(item))
            //    {
            //        parametroVehicle = item;
            //        //var url = $"{baseUrl}/FleetTransport/Vehicle/{item.Id}";
            //        //var response = await Client.DeleteAsync(url);
            //        Debug.WriteLine("A o veiculo foi selecinado com id " + item.Vin);
            //    }
            //    else
            //    {
            //        Debug.WriteLine("veiculo nao foi Selecionado");
            //    }

            //    await CarregarVehiclesAsync();
            //}
            //await AppShell.Current.GoToAsync(nameof(VehicleDetailsPage));
            var navParam = new Dictionary<string, object>();
            navParam.Add("ItemsSelected", parametroVehicle);
            ////await AppShell.Current.Navigation.PushAsync(new VehicleDetailsPage(), navParam);
            ////await Shell.Current.Navigation.PushAsync(new VehicleDetailsPage(), navParam);
            //await AppShell.Current.GoToAsync(nameof(VehicleDetailsPage), navParam);
            await Shell.Current.GoToAsync(nameof(VehicleDetailPage), navParam);

            ////await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetailsPage(), navParam);
            ////await AppShell.Current.GoToAsync(nameof(VehicleDetailsPage), navParam);

            ////GetEmployeesList();

        }

        //[ObservableProperty]
        //public Vehicle _selectedItems;

        //public ICommand RemoveItemCommand => new Command(async () =>
        //     await RemoveItemAsync());

        //public async Task RemoveItemAsync()
        //{
        //    var item = SelectedItems.Id;

        //    var url = $"{baseUrl}/FleetTransport/Vehicle/{item}";
        //    var response = await Client.DeleteAsync(url);
        //    await CarregarVehiclesAsync();

        //}

    }
}
