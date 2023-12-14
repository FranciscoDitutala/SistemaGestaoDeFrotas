
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Map = Microsoft.Maui.Controls.Maps.Map;


namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleLocalizationViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        [ObservableProperty]
        public double _longitude;
        [ObservableProperty]
        public double _latitude;
        [ObservableProperty]
        public string  _address;
        [ObservableProperty]
        public string _description;
        [ObservableProperty]
        public Location _location;

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set
            {
                if(Map != value)
                {
                    _map = value;
                    OnPropertyChanged(nameof(Map));
                }
            }
        }
      
        public VehicleLocalizationViewModel()
        {
            //GetLocalizationMaps();
        }
       
        public ICommand GetLocalizationMapsCommand => new Command(async () =>
             await GetLocalizationMaps());
        private async Task GetLocalizationMaps()
        {
            Location = new Location(Longitude, Latitude);
            Address = "Testando";
            Description = "Testando descricao";
            Map = new Map(MapSpan.FromCenterAndRadius(Location, Distance.FromKilometers(10)));
            
          //  Map.MoveToRegion(MapSpan.FromCenterAndRadius(Location, Distance.FromKilometers(10)));

        }

    }
}
