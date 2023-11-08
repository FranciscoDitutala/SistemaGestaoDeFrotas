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
    public partial class StockyEntriesPageViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
       
      
        public StockyEntriesPageViewModel()
        {
            //SelectedBeginDate = DateTime;
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
        private StockEntry _entrada;
        public StockEntry Entrada
        {
            get { return _entrada; }
            set
            {
                if (_entrada != value)
                {
                    _entrada = value;
                    OnPropertyChanged(nameof(Entrada));
                }
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
        public ICommand CarregarStocksByDateCommand => new Command(async () =>
             await CarregarStocksByDateAsync());
        private async Task CarregarStocksByDateAsync()
        {
            Entrada = new StockEntry();
            var url = $"{baseUrl}/FleetParts/StockEntry/GetStockEntriesByDate";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<StockEntry>
                        (responseStream, _SerializerOptions);
                    Entrada = data;
                    Debug.WriteLine("Carregou com stock  sucesso");
                }
            Debug.WriteLine("Dentro do stock");
        }
        public ObservableCollection<StockEntry> Items { get; set; }
        public ObservableCollection<StockEntry> SelectedItems { get; set; } = new ObservableCollection<StockEntry>();

        public ICommand GoToStockDetailsCommand => new Command(async () =>
           await GoToStockDetailsAsync());
        public async Task GoToStockDetailsAsync()
        {
            Items = new ObservableCollection<StockEntry>(_stock);
            foreach (var item in SelectedItems)
            {
                //var teste = Items.Remove(item);
                if (Items.Contains(item))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new StockDetailsPage(item.Id));
                }

            }
        }
        public ICommand GoToAddStockCommand => new Command(async () =>
          await GoToAddStockAsync());
        public async Task GoToAddStockAsync()
        {
            StockEntry v = new StockEntry();
            await Application.Current.MainPage.Navigation.PushAsync(new StockyEntryPage(v));
        }

      

    }
}
