using Fleet.MauiPrincipal.ViewModel;
using Fleet.MauiPrincipal.Service;
using System.Collections.ObjectModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleListPage : ContentPage
{

    public VehicleListPage()
    {
        InitializeComponent();
        BindingContext = new VehicleListPageViewModel();

    }
    public VehicleListPage(string Token)
    {
        InitializeComponent();
        BindingContext = new VehicleListPageViewModel(Token);
     
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        {
            //_viewModel.GetVehicleListCommand.Execute(null);
        }
       
    }
    private async void GoToVehicleAddPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehicleAddPage());
    }
    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {

    }

    private async void GoToVehicleDetails(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new VehicleDetailPage());
        //await  Shell.Current.GoToAsync(nameof(VehicleDetailsPage));
    }
    private async void UpdateVehicle(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehicleAddPage());
    } 
    private async void GoToAssignVehicle(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehicleAssignPage());
    }

}