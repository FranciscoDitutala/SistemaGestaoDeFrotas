namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAssignPage : ContentPage
{

	public VehicleAssignPage()
	{
		InitializeComponent();
		BindingContext = new ViewModel.VehicleAssignViewModel();
	}

}