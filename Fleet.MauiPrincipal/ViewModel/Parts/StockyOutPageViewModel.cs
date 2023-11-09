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
    public partial class StockyOutPageViewModel :ObservableObject
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

        public StockyOutPageViewModel(StockyOut Outs)
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //stockUpdateId = stockParam;
            CarregarStocksAsync();
            //CarregarSoctkEntryLines();
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

        private ObservableCollection<RequestedLines> _lines = new();
        public ObservableCollection<RequestedLines> StLines
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

            if (Parts.Upc == UpcStock)
            {
                var StockyLines = new RequestedLines()
                {
                    PartUpc = UpcStock,
                    Quantity = QuantStock,
                };
                _lines.Add(StockyLines);
                UpcStock = "";
                QuantStock = 0;
            }
    
        }
        private StockyOut _stockOuts;
        public StockyOut StockOuts
        {
            get { return _stockOuts; }
            set
            {
                _stockOuts = value;
                OnPropertyChanged(nameof(_stockOuts));
            }
        }

        private ObservableCollection<StockyOut> _stock;
        public ObservableCollection<StockyOut> Stock
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
            Stock = new ObservableCollection<StockyOut>();
            var url = $"{baseUrl}/FleetParts/StockEntry/GetStockyEntry/{stockUpdateId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<ObservableCollection<StockyOut>>
                        (responseStream, _SerializerOptions);
                    Stock = data;
       
                    Debug.WriteLine("Carregou com stock de saida  sucesso");
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
             await CadastrarSoctkAsync());
        public async Task CadastrarSoctkAsync()
        {
            VerifyNewStockEntry();
            Debug.WriteLine("Entro no metodo cadastrar Stock o path é ", path);
            var url = $"{baseUrl + path}";
            Debug.WriteLine("Entro no metodo cadastrar Stock a url é " + url);
            
                var StockOuts = new StockyOut
                {
                    RequestedBy = Providers,
                    Notes = Notes,
                    RequestedLines = _lines
                };
                string json = JsonSerializer.Serialize<StockyOut>(StockOuts, _SerializerOptions);
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
                }
                else
                {
                    var response = await Client.PutAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atenção", "Entrada Atualizado com sucesso ", "OK");
                        _lines = null;
                        await Application.Current.MainPage.Navigation.PopAsync();
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                    else { await Application.Current.MainPage.DisplayAlert("Falhou", "Entrada não atualizada  ", "OK"); }
                }
            }
           
        }
    }
