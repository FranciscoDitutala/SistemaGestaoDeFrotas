using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.View.Part;
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
    public class StockyEntriesPageViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        public StockyEntriesPageViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarStocksAsync();
        }
        private List<StockEntry> _stock;
        public List<StockEntry> Stock
        {
            get { return _stock; }
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }

        public ICommand CarregarStocksCommand => new Command(async () =>
             await CarregarStocksAsync());
        private async Task CarregarStocksAsync()
        {
            Stock = new List<StockEntry>();
            var url = $"{baseUrl}/FleetParts/StockEntry/GetStockEntries";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<StockEntry>>
                        (responseStream, _SerializerOptions);
                    Stock = data;
                    Debug.WriteLine("Carregou com stock  sucesso");
                }
            Debug.WriteLine("Dentro do stock");
        }
        public ObservableCollection<StockEntry> Items { get; set; }
        public ObservableCollection<StockEntry> SelectedItems { get; set; } = new ObservableCollection<StockEntry>();

        public ICommand GoToVehicleAddPageCommand => new Command(async () =>
           await GoToVehicleAddPageAsync());
        public async Task GoToVehicleAddPageAsync()
        {
            //await Shell.Current.Go(new AppShell());
            //await Navigation.PushAsync(new AppShell());
        }
    }
}
