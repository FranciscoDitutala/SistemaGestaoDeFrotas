using Fleet.MauiPrincipal.ViewModel;
using System.Runtime.CompilerServices;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAddPage : ContentPage
{	
     private VehicleAddPageViewModel _viewModel;

    public VehicleAddPage()
    {
        InitializeComponent();
    }

    public VehicleAddPage(VehicleAddPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel= viewModel;
        this.BindingContext = _viewModel;

    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

   
}
