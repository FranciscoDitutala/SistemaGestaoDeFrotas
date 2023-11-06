using Fleet.MauiPrincipal.ViewModel;
using Fleet.MauiPrincipal.Service;  
using System.Runtime.CompilerServices;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAddPage : ContentPage
{	
    public VehicleAddPage()
    {
        InitializeComponent();
        //BindingContext = new VehicleAddPageViewModel(Fleet.MauiPrincipal.Service.Vehicle vehicle);
    }

    public VehicleAddPage(Fleet.MauiPrincipal.Service.Vehicle vehicle)
	{
		InitializeComponent();
        BindingContext = new VehicleAddPageViewModel(vehicle);
    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    } 
    

   
}
