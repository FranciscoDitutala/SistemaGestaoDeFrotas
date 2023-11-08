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
    public partial class StockDetailsViewModel:ObservableObject
    {
        public int stockId;
        public StockDetailsViewModel(int Code)
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            stockId = Code;
            Debug.WriteLine("o id passado é " + stockId);
            CarregarStockEntryAsync();
        }
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        
        public ObservableCollection<StockEntry> Items { get; set; }
        public ObservableCollection<StockEntry> SelectedItems { get; set; } = new ObservableCollection<StockEntry>();

        private StockEntry _stock;
        public StockEntry Stock
        {
            get { return _stock; }
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        public ICommand GoToAddStockCommad => new Command(async () =>
         await GoToAddStockAsync());
        private async Task GoToAddStockAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StockyEntryPage(Stock));

        }
        public ICommand CarregarStockEntryCommand => new Command(async () =>
         await CarregarStockEntryAsync());
        private async Task CarregarStockEntryAsync()
        {
            Debug.WriteLine("entrou em carregar stoky " + stockId);
            Stock = new StockEntry();
            var url = $"{baseUrl}/FleetParts/StockEntry/GetStockyEntry/{stockId}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<StockEntry>
                        (responseStream, _SerializerOptions);
                    Stock = data;
                }
        }
    }
}
