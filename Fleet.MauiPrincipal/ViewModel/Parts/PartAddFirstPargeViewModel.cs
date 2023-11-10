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
   public partial class PartAddFirstPargeViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        [ObservableProperty]
        public string _upcPart;
        [ObservableProperty]
        public int _quant;
   
        public PartAddFirstPargeViewModel(string Upc, int Qtd)
        {
            Client = new HttpClient();
            //Stocks = new ObservableCollection<StockEntry>();
            //Parts = new ObservableCollection<Part>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            UpcPart = Upc;
            Quant = Qtd;
            CarregarPartCategoriesAsync();
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
        public ObservableCollection<Categories> Items { get; set; }
        public Categories SelectedItems { get; set; } = new Categories();
        public ICommand CarregarPartCategoriesCommand => new Command(async () =>
             await CarregarPartCategoriesAsync());
        private async Task CarregarPartCategoriesAsync()
        {
            Debug.WriteLine("Entrou no metodo carregar categorias");
            CategoriesItems = new List<Categories>();
            var url = $"{baseUrl}/FleetParts/Part/GetCategories";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Categories>>
                        (responseStream, _SerializerOptions);
                    CategoriesItems = data;
                    Debug.WriteLine("carregaregou categoria com sucesso " + CategoriesItems);
                }
        }
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
        public ICommand GoTOAddPartCommand => new Command(async () =>
         await GoTOAddPartAsync());
        public async Task GoTOAddPartAsync()
        {
            //if (SelectedCategorias.Name.Equals(""))
            //{
                await Application.Current.MainPage.Navigation.PushAsync(new PartAddPage(UpcPart, _selectedCategorias, Quant));
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atenção", "Campos  Obrigatorios Vazio", "Ok");
            //}
           

        }
        public ICommand VoltarCommand => new Command(async () =>
await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
