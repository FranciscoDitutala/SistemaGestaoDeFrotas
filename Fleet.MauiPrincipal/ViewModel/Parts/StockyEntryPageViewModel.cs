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
        public string _UpcPart; 
        [ObservableProperty]
        public string _providers;
        [ObservableProperty]
        public string _Notes;
        [ObservableProperty]
        public double _buyValue;
        [ObservableProperty]
        public int _quant;
        [ObservableProperty]
        public int _quantStock;
        [ObservableProperty]
        public string _upcStock;

        public StockyEntryPageViewModel() {
            Client = new HttpClient();
            Stocks = new ObservableCollection<StockEntry>();
            Parts = new ObservableCollection<Part>();
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
        }

        private ObservableCollection<Part> _parts;
        public ObservableCollection<Part> Parts
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
            Debug.WriteLine("Entrou no metodo carregar peças");
            allItems = new List<Part>();
            var url = $"{baseUrl}/FleetParts/Part/Teste";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Part>>
                        (responseStream, _SerializerOptions);
                    allItems = data;
                    Debug.WriteLine("carregaregou peças com sucesso " + Parts);
                }
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
        private List<StockEntryLines> _lines;
        public List<StockEntryLines> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                OnPropertyChanged();
            }
        }
        public ICommand CadastrarSoctkEntryLinesCommand => new Command(async () =>
             await CadastrarSoctkEntryLinesAsync());
        public async Task CadastrarSoctkEntryLinesAsync()
        {
            var StockyLines = new StockEntryLines
            {
                PartUpc = UpcStock,
                Quantity = QuantStock,
            };
              Lines.Add(StockyLines);
            Debug.WriteLine("O stockyEntryLines é " + StockyLines.PartUpc +" "+ StockyLines.Quantity);
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
            await Application.Current.MainPage.Navigation.PushAsync(new PartAddPage(UpcPart, _selectedCategorias, Quant));
        }

        public ICommand CarregarSoctkEntryLinesCommand => new Command(async () =>
             await CarregarSoctkEntryLines()  );
        public async Task CarregarSoctkEntryLines()
        {
            Lines = Lines;
        }

       public ICommand CadastrarSoctkCommand => new Command(async () =>
             await CadastrarSoctkAsync()  );
        public async Task CadastrarSoctkAsync()
        {
            Debug.WriteLine("Entro no metodo cadastrar Stock");
            var url = $"{baseUrl}/FleetParts/StockEntry/AddStockEntry";
            //if (Quant > 0)
            //{
            //    var StockEntries = new StockEntry
            //    {
            //        ProvidersInfo = Providers,
            //        Notes = Notes,
            //        TotalValue = BuyValue,
            //        Lines = Lines
            //    };
            //    Debug.WriteLine("Entrou no if");
            //    string json = JsonSerializer.Serialize<StockEntry>(StockEntries, _SerializerOptions);
            //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //    var response = await Client.PostAsync(url, content);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        await Application.Current.MainPage.DisplayAlert("Atenção", "Stock cadastrado  com sucesso ", "OK");
            //        //teste = Atribuir.Id;
            //    }
            //    else { await Application.Current.MainPage.DisplayAlert("Falhou", "Stock não cadastrado ", "OK"); }

            //}
            //else { await Application.Current.MainPage.DisplayAlert("Atenção", "Campos obrigatorio Vazio ", "OK"); }

        }





    }



}
