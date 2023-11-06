 using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.View.Part;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Collections;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Input;
using System.ComponentModel;


namespace Fleet.MauiPrincipal.ViewModel.Parts
{
   
    public partial class PartCategoriesViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        //public byte[] arryByte = new byte[];
        //public ImageSource imagem = ImageSource.FromStream(() => new MemoryStream(arryByte));

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
        public PartCategoriesViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarPartCategoriesAsync();
        }

        public ObservableCollection<Categories> Items { get; set; }
        public ObservableCollection<Categories> SelectedItems { get; set; } = new ObservableCollection<Categories>();
        //private Categories _selectedCategorias;
        //public Categories SelectedCategorias
        //{
        //    get { return _selectedCategorias; }
        //    set
        //    {
        //        if (SelectedCategorias != value)
        //        {
        //            _selectedCategorias = value;
        //            OnPropertyChanged(nameof(SelectedCategorias));
        //        }
        //    }
        //}
        public ICommand GoToPartTypeNameCommand => new Command(async () =>
             await GoToPartTypeNameAsync());
        private async Task GoToPartTypeNameAsync()
        {
            Items = new ObservableCollection<Categories>(_categoriesItems);
            foreach (var item in SelectedItems)
            {
                //var teste = Items.Remove(item);
                if (Items.Contains(item))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new PartCategoryPage(item));
                }
                //else { await Application.Current.MainPage.DisplayAlert("Atencao ","Nao teve exito a funcao","Ok"); }

            }
            //await Application.Current.MainPage.Navigation.PushAsync(new PartCategoryPage(_selectedCategorias));
        }
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
        public PartTypeCategory partCategoria;
        private List<PartTypeCategory> _partCategoriesItems;
        public List<PartTypeCategory> PartCategoriesItems
        {
            get { return _partCategoriesItems; }
            set
            {
                _partCategoriesItems = value;
                OnPropertyChanged(nameof(PartCategoriesItems));
            }
        }
    }


















    //public ICommand CarregarCategoriesCommand => new Command(async () =>
    //     await CarregarCategoriesAsync());
    //private async Task CarregarCategoriesAsync()
    //{

    //    Items = new ObservableCollection<Categories>(_categoriesItems);
    //    foreach (var item in SelectedItems)
    //    {
    //        Debug.WriteLine("O TAMANHO DO selecteed items" + SelectedItems.Count);

    //        //var teste = Items.Remove(item);
    //        if (Items.Contains(item))
    //        {
    //            var url = $"{baseUrl}/FleetParts/Part/GetTypesByCategory/{item.Name}";
    //            var response = await Client.GetAsync(url);
    //            if (response.IsSuccessStatusCode)
    //                using (var responseStream = await response.Content.ReadAsStreamAsync())
    //                {
    //                    var data = await JsonSerializer.DeserializeAsync<List<Categories>>
    //                        (responseStream, _SerializerOptions);
    //                    CategoriesItems = data;
    //                    Debug.WriteLine("carregaregou categoria com sucesso " + CategoriesItems);
    //                }
    //        }  
    //    }

}
