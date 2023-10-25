using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.Parts
{
    public class StockyEntryPageViewModel : ObservableObject
    {

        public StockyEntryPageViewModel(){

        } 
        public ICommand SalvarStockCommand => new Command(async () =>
          await SalvarStockCommandAsync());
        public async Task SalvarStockCommandAsync()
        {
            //await Shell.Current.Go(new AppShell());
            //await Navigation.PushAsync(new AppShell());
        }
    }

   

}
