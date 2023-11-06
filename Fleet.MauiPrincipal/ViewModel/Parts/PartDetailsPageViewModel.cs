using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service.Part;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.Parts
{
    public partial class PartDetailsPageViewModel: ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        //[ObservableProperty]
        //public string _estadoAtribuicao;
        //[ObservableProperty]
        //public string _funcionario;
        //[ObservableProperty]
        //public string _departamento;

        public string partCode;
        public PartDetailsPageViewModel(string Code)
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            partCode = Code;
            CarregarPartsAsync();
        }
        public ObservableCollection<Part> Items { get; set; }
        public ObservableCollection<Part> SelectedItems { get; set; } = new ObservableCollection<Part>();

        private Part _part;
        public Part Parts
        {
            get { return _part; }
            set
            {
                _part = value;
                OnPropertyChanged(nameof(Parts));
            }
        }

        public ICommand CarregarPartsCommand => new Command(async () =>
         await CarregarPartsAsync());
        private async Task CarregarPartsAsync()
        {
            Parts = new Part();
            var url = $"{baseUrl}/FleetParts/Part/{partCode}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<Part>
                        (responseStream, _SerializerOptions);
                    Parts = data;
                }
        }
    }
}
