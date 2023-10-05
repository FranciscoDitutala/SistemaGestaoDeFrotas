using Fleet.MauiPrincipal.ViewModel;
using System.Runtime.CompilerServices;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAddPage : ContentPage
{	
     private VehicleAddPageViewModel _viewModel;

    public VehicleAddPage()
    {
    }

    public VehicleAddPage(VehicleAddPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel= viewModel;
        this.BindingContext = _viewModel;

    }
    
}
