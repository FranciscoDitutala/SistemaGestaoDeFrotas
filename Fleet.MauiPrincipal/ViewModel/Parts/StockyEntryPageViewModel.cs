using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
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
    public partial class StockyEntryPageViewModel : ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
       
        [ObservableProperty]
        public string _providers;
        [ObservableProperty]
        public string _notes;
        [ObservableProperty]
        public double _buyValue;
    
        [ObservableProperty]
        public int _quantStock;
        [ObservableProperty]
        public string _upcStock;
        public StockEntry stockUpdateId;

        public StockyEntryPageViewModel(StockEntry stockParam) {
          
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            stockUpdateId = stockParam;
            FillTheFields();
            CarregarStocksAsync();
            CarregarSoctkEntryLines();
        }

        private Part _parts;
        public Part Parts
        {
            get { return _parts; }
            set
            {
                if (_parts != value)
                {
                    _parts = value;
                    OnPropertyChanged(nameof(Parts));
                }
            }
        }
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
               if (_searchText != value){
                    _searchText = value;
                    OnPropertyChanged();
                    FilterItems();
                }
            }
        }
        private ObservableCollection<Part> items;
        public ObservableCollection<Part> Items
        {
            get => items;
            set
            {
                if (items != value)
                {
                    items = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<Part> allItems;

        private void FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Items = new ObservableCollection<Part>(allItems);
            }
            else
            {
                var filteredItems = allItems.Where(item => item.Upc.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
                Items = new ObservableCollection<Part>(filteredItems);
            }
        }
      
        private ObservableCollection<StockEntryLines> _lines = new ();
        public ObservableCollection<StockEntryLines> StLines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                OnPropertyChanged(nameof(StLines));
            }
        } 
        public ICommand CadastrarSoctkEntryLinesCommand => new Command(async () =>
             await CadastrarSoctkEntryLinesAsync());
        public async Task CadastrarSoctkEntryLinesAsync()
        {
            Debug.WriteLine("Entrou no metodo carregar peças");
            Parts = new Part();
            var url = $"{baseUrl}/FleetParts/Part/{UpcStock}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<Part>
                        (responseStream, _SerializerOptions);
                    Parts = data;
                    Debug.WriteLine("carregaregou peças com sucesso " + Parts);
                }

            if (Parts.Upc == UpcStock) {
                var StockyLines = new StockEntryLines
                {
                    PartUpc = UpcStock,
                    Quantity = QuantStock,
                };
                _lines.Add(StockyLines);
                UpcStock = "";
                QuantStock=0;
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PartAddFirstParge(UpcStock, QuantStock));
            } 
         }
        private StockEntry _stockEntry;
        public StockEntry StockEntries
        {
            get { return _stockEntry; }
            set
            {
                _stockEntry = value;
                OnPropertyChanged(nameof(StockEntries));
            }
        }
        public ICommand GoTOAddPartCommand => new Command(async () =>
          await GoTOAddPartAsync());
        public async Task GoTOAddPartAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PartAddFirstParge(UpcStock, QuantStock));
        }
        public ICommand CarregarSoctkEntryLinesCommand => new Command(async () =>
             await CarregarSoctkEntryLines()  );
        public async Task CarregarSoctkEntryLines()
        {
            StLines = _lines;
        }
        private ObservableCollection< StockEntry> _stock;
        public ObservableCollection<StockEntry> Stock
        {
            get { return _stock; }
            set
            {
                _stock = value;
                OnPropertyChanged(nameof(Stock));
            }
        }
        private string path;
        int paramId;
        public bool isNewItem;
        public ICommand CarregarStocksCommand => new Command(async () =>
            await CarregarStocksAsync());
        private async Task CarregarStocksAsync()
        {
            Stock = new ObservableCollection<StockEntry>();
            var url = $"{baseUrl}/FleetParts/StockEntry/GetStockyEntry/{stockUpdateId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<StockEntry>>
                        (responseStream, _SerializerOptions);
                    Stock = data;
                    //isNewItem = false;
                    //path = $"/FleetParts/StockEntry/UpdateStockEntry/{Stock.Id}";
                    Debug.WriteLine("Carregou com stock de entrada  sucesso");
                }
            else {
                //isNewItem = true;
                //path = "/FleetParts/StockEntry/AddStockEntry";
            }
            Debug.WriteLine("Dentro do stock");
        }
        public void VerifyNewStockEntry()
        {
            foreach (var item in Stock)
            {
                if (!item.Id.Equals(stockUpdateId.Id))
                {
                    isNewItem = true;

                    path = "/FleetTransport/Vehicle";
                    Debug.WriteLine("Chegou aqui ", path);
                }
                else
                {
                    isNewItem = false;
                    path = $"/FleetTransport/Vehicle/{item.Id}";
                }
            }   
        }
       
        public ICommand CadastrarSoctkCommand => new Command(async () =>
             await CadastrarSoctkAsync() );
        public async Task CadastrarSoctkAsync()
        {
            VerifyNewStockEntry();
            Debug.WriteLine("Entro no metodo cadastrar Stock o path é ", path);
            var url = $"{baseUrl + path}";
            Debug.WriteLine("Entro no metodo cadastrar Stock a url é " + url);
            if (BuyValue > 0)
            {
                var StockEntries = new StockEntry
                {
                    ProvidersInfo = Providers,
                    Notes = Notes,
                    TotalValue = BuyValue,
                    Lines = _lines
                };
                string json = JsonSerializer.Serialize<StockEntry>(StockEntries, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                if (isNewItem)
                {
                    var response = await Client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", "Entrada cadastrado  com sucesso ", "OK");
                        _lines = null;
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                    else { await Application.Current.MainPage.DisplayAlert("Falhou", "Entrada não cadastrado ", "OK"); }
                }else
                {
                    var response = await Client.PutAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", "Entrada Atualizado com sucesso ", "OK");
                        _lines = null;
                        ClearTheFields();
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                    else { await Application.Current.MainPage.DisplayAlert("Falhou", "Entrada não atualizada  ", "OK"); }
                }
            }
            else { await Application.Current.MainPage.DisplayAlert("Atenção", "Campos obrigatorio Vazio ", "OK"); }
        }
        public void FillTheFields() {
            //Providers = stockUpdateId.ProvidersInfo;
            //Notes = stockUpdateId.Notes;
            //BuyValue = stockUpdateId.TotalValue;
            //StLines = stockUpdateId.Lines;
        }
        public void ClearTheFields()
        {
            Providers = "";
            Notes = "";
            BuyValue =0;
            _lines = null;
        }
    }
}
