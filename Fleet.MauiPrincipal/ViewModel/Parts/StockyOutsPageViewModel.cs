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
    public partial class StockyOutsPageViewModel :ObservableObject
    {
  
    private HttpClient Client;
    JsonSerializerOptions _SerializerOptions;
    string baseUrl = "https://localhost:7111";


    public StockyOutsPageViewModel()
    {
        //SelectedBeginDate = DateTime;
        Client = new HttpClient();
        _SerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        CarregarStocksAsync();
    }
    private List<StockyOut> _stock;
    public List<StockyOut> Stock
    {
        get { return _stock; }
        set
        {
            _stock = value;
            OnPropertyChanged(nameof(Stock));
        }
    }
    private StockyOut _entrada;
    public StockyOut Entrada
    {
        get { return _entrada; }
        set
        {
            if (_entrada != value)
            {
                _entrada = value;
                OnPropertyChanged(nameof(Entrada));
            }
        }
    }

    public ICommand CarregarStocksCommand => new Command(async () =>
         await CarregarStocksAsync());
    private async Task CarregarStocksAsync()
    {
        Stock = new List<StockyOut>();
        var url = $"{baseUrl}/FleetParts/StockEntry/GetStockEntries";
        var response = await Client.GetAsync(url);
        if (response.IsSuccessStatusCode)
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<List<StockyOut>>
                    (responseStream, _SerializerOptions);
                Stock = data;
                Debug.WriteLine("Carregou com stock  sucesso");
            }
        Debug.WriteLine("Dentro do stock");
    }
    public ICommand CarregarStocksByDateCommand => new Command(async () =>
         await CarregarStocksByDateAsync());
    private async Task CarregarStocksByDateAsync()
    {
        Entrada = new StockyOut();
        var url = $"{baseUrl}/api/StockOut";
        var response = await Client.GetAsync(url);
        if (response.IsSuccessStatusCode)
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var data = await JsonSerializer.DeserializeAsync<StockyOut>
                    (responseStream, _SerializerOptions);
                Entrada = data;
                Debug.WriteLine("Carregou com stock  sucesso");
            }
        Debug.WriteLine("Dentro do stock");
    }
    public ObservableCollection<StockyOut> Items { get; set; }
    public ObservableCollection<StockyOut> SelectedItems { get; set; } = new ObservableCollection<StockyOut>();

    public ICommand GoToStockDetailsCommand => new Command(async () =>
       await GoToStockDetailsAsync());
    public async Task GoToStockDetailsAsync()
    {
        Items = new ObservableCollection<StockyOut>(_stock);
        foreach (var item in SelectedItems)
        {
            //var teste = Items.Remove(item);
            if (Items.Contains(item))
            {
                //await Application.Current.MainPage.Navigation.PushAsync(new StockDetailsPage(item.Id));
            }

        }
    }
    public ICommand GoToAddStockCommand => new Command(async () =>
      await GoToAddStockAsync());
    public async Task GoToAddStockAsync()
    {
            StockyOut v = new StockyOut();
            await Application.Current.MainPage.Navigation.PushAsync(new StockyOutPage(v));
        }



}
}
