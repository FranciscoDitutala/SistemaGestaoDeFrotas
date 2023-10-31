using Fleet.MauiPrincipal.ViewModel;
using Fleet.MauiPrincipal.Service;
using System.Runtime.CompilerServices;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAddPage : ContentPage
{	
     private VehicleAddPageViewModel _viewModel;

    public VehicleAddPage()
    {
        InitializeComponent();
    }

    public VehicleAddPage(Fleet.MauiPrincipal.Service.Vehicle vehicle)
	{
		InitializeComponent();
		//_viewModel= viewModel;
        this.BindingContext =new VehicleAddPageViewModel(vehicle);

    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    } 
    
    //private async void ReturnToListVehicle(object sender, EventArgs e)
    //{
    //    await Navigation.PopAsync();
    //}

   
}
