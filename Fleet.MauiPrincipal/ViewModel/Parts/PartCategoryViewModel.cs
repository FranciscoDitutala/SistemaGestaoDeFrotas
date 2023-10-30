using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Fleet.MauiPrincipal.Service.Part;
using CommunityToolkit.Mvvm.Collections;
using System.Text.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;



namespace Fleet.MauiPrincipal.ViewModel.Parts
{
    
    public partial class PartCategoryViewModel: ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        public PartTypeCategory categoria;
        public string partTypename;
        private List<PartTypeCategory> _typeVategoriesItems;
        public List<PartTypeCategory> TypeCategoriesItems
        {
            get { return _typeVategoriesItems; }
            set
            {
                _typeVategoriesItems = value;
                OnPropertyChanged(nameof(TypeCategoriesItems));
            }
        }
        public PartCategoryViewModel(string TypeNameCategory)
        {
            partTypename = TypeNameCategory;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CarregarPartTypeCategoriesAsync();
        }

        public ObservableCollection<PartTypeCategory> Items { get; set; }
        public ObservableCollection<PartTypeCategory> SelectedItems { get; set; } = new ObservableCollection<PartTypeCategory>();
        public ICommand CarregarPartTypeCategoriesCommand => new Command(async () =>
             await CarregarPartTypeCategoriesAsync());
        private async Task CarregarPartTypeCategoriesAsync()
        {
            Debug.WriteLine("Entrou no metodo carregar TypeCategorias da partCategoriasViewModel " +
                "o valor do parametro é" + partTypename);
            TypeCategoriesItems = new List<PartTypeCategory>();
            var url = $"{baseUrl}/FleetParts/Part/GetTypesByCategory/{partTypename}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<PartTypeCategory>>
                        (responseStream, _SerializerOptions);
                    TypeCategoriesItems = data;
                    Debug.WriteLine("carregou categoria com sucesso " + TypeCategoriesItems);

                }
            else { Debug.WriteLine("Não Carregou acategoria com sucesso " + TypeCategoriesItems); }
        }

    }
}
