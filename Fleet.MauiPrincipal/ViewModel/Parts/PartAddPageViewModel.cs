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
    public partial class PartAddPageViewModel : ObservableObject
    {

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        [ObservableProperty]
        public string _upc;
        [ObservableProperty]
        public string _sku;
        [ObservableProperty]
        public string _model;
        [ObservableProperty]
        public string _brand;
        [ObservableProperty]
        public int _quant;
        [ObservableProperty]
        public string _catName;
        [ObservableProperty]
        public ObservableCollection<Part> _parts;

        //public PartAddPageViewModel() {
        //    Client = new HttpClient();
        //    Parts = new ObservableCollection<Part>();
        //    _SerializerOptions = new JsonSerializerOptions
        //    {
        //        PropertyNameCaseInsensitive = true
        //    };
         
        //}
        public PartAddPageViewModel(String UPC, Categories Cat, int Qtd)
        {
            Client = new HttpClient();
            Parts = new ObservableCollection<Part>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Debug.WriteLine("Dados passados por parametro sao " + UPC, +Qtd);
            Upc = UPC;
            Quant = Qtd;
            CatName = Cat.Name;
            CarregarPartCategoriesAsync();

        }
        public PartTypeCategory categoria;
        private List<PartTypeCategory> _categoriesItems;
        public List<PartTypeCategory> CategoriesItems
        {
            get { return _categoriesItems; }
            set
            {
                _categoriesItems = value;
                OnPropertyChanged(nameof(CategoriesItems));
            }
        }

        public ObservableCollection<PartTypeCategory> Items { get; set; }
        public PartTypeCategory SelectedItems { get; set; } = new PartTypeCategory();
        public ICommand CarregarPartCategoriesCommand => new Command(async () =>
             await CarregarPartCategoriesAsync());
        private async Task CarregarPartCategoriesAsync()
        {
            Debug.WriteLine("Entrou no metodo carregar categorias");
            CategoriesItems = new List<PartTypeCategory>();
            var url = $"{baseUrl}/FleetParts/Part/GetTypesByCategory/{CatName}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<PartTypeCategory>>
                        (responseStream, _SerializerOptions);
                    CategoriesItems = data;
                    Debug.WriteLine("carregaregou categoria com sucesso " + CategoriesItems);
                }
        }

        //private List<Part> _partItem;
        //public List<Part> PartItems
        //{
        //    get { return _partItem; }
        //    set
        //    {
        //        _partItem = value;
        //        OnPropertyChanged(nameof(PartItems));
        //    }
        //}
        //public ICommand CarregarPartCommad  => new Command(async () =>
        //     await CarregarPartAsync());
        //private async Task CarregarPartAsync()
        //{
        //    Debug.WriteLine("Entrou no metodo carregar categorias");
        //    PartItems = new List<Part>();
        //    var url = $"{baseUrl}/FleetParts/Part";
        //    var response = await Client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //        using (var responseStream = await response.Content.ReadAsStreamAsync())
        //        {
        //            var data = await JsonSerializer.DeserializeAsync<List<Part>>
        //                (responseStream, _SerializerOptions);
        //            PartItems = data;
        //            Debug.WriteLine("carregaregou categoria com sucesso " + CategoriesItems);
        //        }
        //}

        private PartTypeCategory _selectedCategorias;
        public PartTypeCategory SelectedCategorias
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

        public ICommand CadastraPartCommand => new Command(async () =>
         await CadastraPartAsync());
        public bool isNewItem = false;
        private async Task CadastraPartAsync()
        {
            if(Quant>0)
            {
                var part = new Part
                {
                    PartTypeName = _selectedCategorias.Name,
                    Brand = Brand,
                    Model = Model,
                    Sku = Sku,
                    Upc = Upc,
                    StockQty = Quant
                };
                var url = $"{baseUrl}/FleetParts/Part";
                string json = JsonSerializer.Serialize<Part>(part, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("Cadastrado com sucesso" + part.Id);
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Cadastro feito", "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            } else { await Application.Current.MainPage.DisplayAlert("Atenção","Campos Obrigatorios em branco","Ok"); }    
        }

    }
}
