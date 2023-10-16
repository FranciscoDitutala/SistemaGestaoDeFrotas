using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
 
    public partial class VehicleBrandViewModel : ObservableObject , INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        static Random random = new();
        public ObservableCollection<VehicleBrand> BrandItems { get; } = new();

        private List<VehicleBrand> _brands;
        public List<VehicleBrand> Brands
        {
            get { return _brands; }
            set
            {
                _brands = value;
                OnPropertyChanged(nameof(Brands));
            }
        }
        private Image _logoImage;
        public Image LogoBrands
        {
            get { return _logoImage; }
            set
            {
                _logoImage = value;
                OnPropertyChanged(nameof(LogoBrands));
            }
        }


        [ObservableProperty]
        public string _nameBrands;
        [ObservableProperty]
        public Image _logoBrand = new Image();


        [ObservableProperty]
        public string _logoName;
        [ObservableProperty]
        public string _companyBrands;

        public VehicleBrandViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarBrandsAsync();
            Debug.WriteLine(CarregarBrandsAsync());
        }

        public ICommand CarregarBrandsCommand => new Command(async () =>
             await CarregarBrandsAsync());
        private async Task CarregarBrandsAsync()
        {
            Debug.WriteLine("Entrou no metodo carregar marca");
            Brands = new List<VehicleBrand>();
            var url = $"{baseUrl}/FleetCommon/VehicleBrand";
            var response = await Client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<VehicleBrand>>
                        (responseStream, _SerializerOptions);

                    Brands = data;

                    Debug.WriteLine("As informacoes da lista " +Brands);
                }
            else
            { Debug.WriteLine("Nao teve sucessso ao carregar "); }
            Debug.WriteLine("Saiu do if da verificação da Marca " + Brands);
        }

        public ObservableCollection<VehicleBrand> ItemsBrand { get; set; }
        public ObservableCollection<VehicleBrand> SelectedBrands { get; set; } = new ObservableCollection<VehicleBrand>();

        public ICommand RemoveBrandsCommand => new Command(async () =>
        {
            Debug.WriteLine("Chamou o metodo Remover Brands nao async");
            await DeletarVehicleBrandAsync();
        });
    
        private async Task DeletarVehicleBrandAsync()
        {
            Debug.WriteLine("Entrou no metodo apagar brands");
            ItemsBrand = new ObservableCollection<VehicleBrand>(_brands);
            foreach (var item in SelectedBrands)
            {
                Debug.WriteLine("O TAMANHO DO selecteed items" + SelectedBrands.Count);

                //var teste = Items.Remove(item);
                if (ItemsBrand.Contains(item))
                {
                    var url = $"{baseUrl}/FleetCommon/VehicleBrand/{item.Id}";
                    var response = await Client.DeleteAsync(url);
                    Debug.WriteLine("A o veiculo foi apagado com id " + item.Id);
                }
                else
                {
                    Debug.WriteLine("veiculo nao foi apagado");
                }

                await CarregarBrandsAsync();
            }
        }


        public ICommand CadastraBrandsModelCommand => new Command(async () =>
          await CadastraBrandsModelAsync());
        private async Task CadastraBrandsModelAsync()
        {
            if (!string.IsNullOrEmpty(NameBrands))
            {
                var brandItem = new VehicleBrand()
                {
                    Name = NameBrands,
                    Company = CompanyBrands,
                    Logo = LogoName,
                };
                var url = $"{baseUrl}/FleetCommon/VehicleBrand";
                string json = JsonSerializer.Serialize<VehicleBrand>(brandItem, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                Debug.WriteLine("A marca foi cadastrado com sucesso " + json);
                await CarregarBrandsAsync();
            }
        }

        public ICommand CarregarImagemCommand => new Command(async () =>
         await CarregarImagem());
        public async Task CarregarImagem()
        {
            var result = await FilePicker.PickAsync(
                new PickOptions
                {
                    PickerTitle = "Logotipo",
                    FileTypes = FilePickerFileType.Images
                }
                );
            if (result == null)
                return;
            var stream = await result.OpenReadAsync();
            LogoBrands = (new Image
            {
                Source = ImageSource.FromStream(() => stream)
            });
        }

    }
}
