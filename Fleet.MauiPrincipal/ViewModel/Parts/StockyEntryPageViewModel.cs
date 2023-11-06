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
        public ObservableCollection<StockEntry> _stocks;
       
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

        public StockyEntryPageViewModel() {
            Client = new HttpClient();
            Stocks = new ObservableCollection<StockEntry>();
            //Parts = new ObservableCollection<Part>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //allItems = new List<Part> { 
            //    new Part { Upc = "Teste", Sku = "Gets", Brand = "Teste", PartTypeName = "Teste", Model = "Teste", StockQty = 4 },
            //    new Part { Upc = "Testador", Sku = "Testador", Brand = "Teste", PartTypeName = "Teste", Model = "Teste", StockQty = 4 },
            //    };
           
            //Items = new ObservableCollection<Part>(allItems);
            //CarregarPartCategoriesAsync();
            CarregarPartAsync();
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
        public ICommand CarregarPartCommand => new Command(async () =>
             await CarregarPartAsync());
        private async Task CarregarPartAsync()
        {
           
        }
        public Categories categoria;
        private List<Categories> _categoriesItems;
        public List<Categories> CategoriesItems
        {
            get { return _categoriesItems; }
            set
            {
                _categoriesItems = value;
                OnPropertyChanged(nameof(CategoriesItems));
            }
        }
        //public ObservableCollection<Categories> Items { get; set; }  
        //public Categories SelectedItems { get; set; } = new Categories();
        //public ICommand CarregarPartCategoriesCommand => new Command(async () =>
        //     await CarregarPartCategoriesAsync());
        //private async Task CarregarPartCategoriesAsync()
        //{
        //    Debug.WriteLine("Entrou no metodo carregar categorias");
        //    CategoriesItems = new List<Categories>();
        //    var url = $"{baseUrl}/FleetParts/Part/GetCategories";
        //    var response = await Client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //        using (var responseStream = await response.Content.ReadAsStreamAsync())
        //        {
        //            var data = await JsonSerializer.DeserializeAsync<List<Categories>>
        //                (responseStream, _SerializerOptions);
        //            CategoriesItems = data;
        //            Debug.WriteLine("carregaregou categoria com sucesso " + CategoriesItems);
        //        }
        //}
        private Categories _selectedCategorias;
        public Categories SelectedCategorias
        {
            get { return _selectedCategorias; }
            set
            {
                if (SelectedCategorias != value)
                {
                    _selectedCategorias = value;
                    OnPropertyChanged(nameof(SelectedCategorias));
                }
              

            }
        }
        public ICommand SalvarStockCommand => new Command(async () =>
                 await SalvarStockCommandAsync());
        public async Task SalvarStockCommandAsync()
        {
            
        }
        //public StockEntryLines StockyLines;
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
                //foreach (var item in StLines)
                //{
                //    Debug.WriteLine("O stockyEntryLines a quantidade " + item.Quantity + " o codigo" + item.PartUpc);
                //}
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
            //await Application.Current.MainPage.Navigation.PushAsync(new PartAddPage(UpcPart, _selectedCategorias, Quant));     
            await Application.Current.MainPage.Navigation.PushAsync(new PartAddFirstParge(UpcStock, QuantStock));
        }
        public ICommand CarregarSoctkEntryLinesCommand => new Command(async () =>
             await CarregarSoctkEntryLines()  );
        public async Task CarregarSoctkEntryLines()
        {
            StLines = _lines;
        }

       public ICommand CadastrarSoctkCommand => new Command(async () =>
             await CadastrarSoctkAsync()  );
        public async Task CadastrarSoctkAsync()
        {
            Debug.WriteLine("Entro no metodo cadastrar Stock");
            var url = $"{baseUrl}/FleetParts/StockEntry/AddStockEntry";
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
                var response = await Client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Stock cadastrado  com sucesso ", "OK");
                    StLines = null;
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else { await Application.Current.MainPage.DisplayAlert("Falhou", "Stock não cadastrado ", "OK"); }

            }
            else { await Application.Current.MainPage.DisplayAlert("Atenção", "Campos obrigatorio Vazio ", "OK"); }

        }

    }



}
