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
    public partial class PartPageViewModel :ObservableObject
    {

        public PartPageViewModel()
        {

        }
        public ICommand GoToStockOutsCommand => new Command(async () =>
           await GoToStockOutsAsync());
        public async Task GoToStockOutsAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StockyOutsPage());
        } 
        public ICommand GoToStockEntriesCommand => new Command(async () =>
           await GoToStockEntriesAsync());
        public async Task GoToStockEntriesAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new StockyEntriesPage());
        } 
        public ICommand GoToPartCategoriesCommand => new Command(async () =>
           await GoToPartCategoriesAsync());
        public async Task GoToPartCategoriesAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PartCategoriesPage());
        }
    }
}