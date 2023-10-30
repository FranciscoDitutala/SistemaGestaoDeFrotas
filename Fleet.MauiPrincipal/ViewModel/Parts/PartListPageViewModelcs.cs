using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service.Part;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.Parts
{
    public partial class PartListPageViewModelcs:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        static Random random = new();
        public ObservableCollection<Part> PartItems { get; } = new();

        private List<Part> _part;
        public List<Part> Parts
        {
            get { return _part; }
            set
            {
                _part = value;
                OnPropertyChanged(nameof(Parts));
            }
        }

        public PartListPageViewModelcs ()
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
            Parts = new List<Part>();
            var url = $"{baseUrl}/FleetParts/Part/1";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Part>>
                        (responseStream, _SerializerOptions);

                    Parts = data;
                }
        }
        public ObservableCollection<Part> Items { get; set; }
        public ObservableCollection<Part> SelectedItems { get; set; } = new ObservableCollection<Part>();

        public Part parametroVehicle { get; set; }
        //public Part SelectedItems { get; set; } = new Part();
        //public ICommand RemoveSelectedCommand { get; set; }

        public ICommand RemoveSelectedCommand => new Command(async () =>
             await DeletarVehiclesAsync());
        private async Task DeletarVehiclesAsync()
        {
            Items = new ObservableCollection<Part>(_part);
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
        public async Task GoToVehicleAddPageAsync()
        {
            //await Shell.Current.Go(new AppShell());
            //await Navigation.PushAsync(new AppShell());

            //var navParam = new Dictionary<string, object>();
            //navParam.Add("AddEmployee", employee);
            //await AppShell.Current.GoToAsync(nameof(AddEmployee), navParam);
            //GetEmployeesList();

        }

    }

}
