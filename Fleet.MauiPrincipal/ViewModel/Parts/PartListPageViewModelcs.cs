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
    public partial class PartListPageViewModelcs:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        public ObservableCollection<Part> PartItems { get; } = new();
        public PartTypeCategory typeCategory;
        private List<Part> _part;
        public List<Part> Parts
        {
            get { return _part; }
            set
            {
                _part = value;
                OnPropertyChanged(nameof(Parts));
            }
        }

        public PartListPageViewModelcs (PartTypeCategory TypeCategory)
        {
            typeCategory = TypeCategory;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarPartsAsync();
        }
        public ICommand CarregarPartsCommand => new Command(async () =>
             await CarregarPartsAsync());
        private async Task CarregarPartsAsync()
        {
            Parts = new List<Part>();
            var url = $"{baseUrl}/FleetParts/Part/GetPartsByType/{typeCategory.Name}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Part>>
                        (responseStream, _SerializerOptions);

                    Parts = data;
                }
        }
        public ObservableCollection<Part> Items { get; set; }
        public Part SelectedItems { get; set; } = new Part();
        public Part parametroVehicle { get; set; }
        public ICommand GoToPartDetalhesCommand => new Command(async () =>
             await GoToPartDetalhesAsync());
        public async Task GoToPartDetalhesAsync()
        {
            Items = new ObservableCollection<Part>(_part);
            foreach (var item in Items)
            {
                if (item.Id.Equals(SelectedItems.Id))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new PartDetailsPage(item.Upc));
                }
            }
        }

    }

}
