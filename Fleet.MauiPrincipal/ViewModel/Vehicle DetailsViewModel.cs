using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.View.Vehicle;
using Fleet.MauiPrincipal.Service;
using System.Diagnostics;

namespace Fleet.MauiPrincipal.ViewModel
{
    [QueryProperty(nameof(VehicleListPage), "ItemsSelected")]
    public partial class Vehicle_DetailsViewModel: ObservableObject
    {
        [ObservableProperty]
        private Vehicle _itemsSelected = new Vehicle();

        [ObservableProperty]
        public string _matricula;

        public Vehicle_DetailsViewModel()
        {
            //Matricula = ItemsSelected.Vin;
            //Debug.WriteLine("o valor do chassis é " + Matricula);
        }



        // public ICommand GoToVehicleDeataisCommand => new Command(async () =>
        //await GoToVehicleDeataisAsync());
        //public async Task GoToVehicleDeataisAsync()
        //{
        //    //await Shell.Current.Go(new AppShell());
        //    //await Navigation.PushAsync(new AppShell());

        //    //var navParam = new Dictionary<string, object>();
        //    //navParam.Add("AddEmployee", employee);
        //    //await AppShell.Current.GoToAsync(nameof(AddEmployee), navParam);
        //    //GetEmployeesList();
        //    foreach (var item in SelectedItems)
        //    {

        //    }    
        //}
    }
}
