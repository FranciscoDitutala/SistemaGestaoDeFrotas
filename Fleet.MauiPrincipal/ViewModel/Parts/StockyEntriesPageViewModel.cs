using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.View.Part;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.Parts
{
    public class StockyEntriesPageViewModel:ObservableObject
    {
        public StockyEntriesPageViewModel()
        {

        }

        public ICommand GoToVehicleAddPageCommand => new Command(async () =>
           await GoToVehicleAddPageAsync());
        public async Task GoToVehicleAddPageAsync()
        {
            //await Shell.Current.Go(new AppShell());
            //await Navigation.PushAsync(new AppShell());
        }
    }
}
