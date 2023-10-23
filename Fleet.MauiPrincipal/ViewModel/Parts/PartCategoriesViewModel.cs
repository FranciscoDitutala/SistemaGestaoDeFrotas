using Fleet.MauiPrincipal.Service.Part;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Collections;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Input;
using System.ComponentModel;


namespace Fleet.MauiPrincipal.ViewModel.Parts
{
   
    public partial class PartCategoriesViewModel:ObservableObject, INotifyPropertyChanged
    {
        static Random random = new();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
 
        //public List<Categories> categoriesItems { get; private set; }
        //public List<Categories> CategoriesItems
        //{
        //    get { return categoriesItems; }
        //    set { categoriesItems = value; }
        //}
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

        public ICommand CarregarCategoriesCommand => new Command(async () =>
             await CarregarCategoriesAsync());
        private async Task CarregarCategoriesAsync()
        {

            Items = new ObservableCollection<Categories>(_categoriesItems);
            foreach (var item in SelectedItems)
            {
                Debug.WriteLine("O TAMANHO DO selecteed items" + SelectedItems.Count);

                //var teste = Items.Remove(item);
                if (Items.Contains(item))
                {
                    var url = $"{baseUrl}/FleetParts/Part/GetTypesByCategory/{item.Name}";
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
            }
              
        }

    }
}
