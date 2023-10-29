using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleDetailPage : ContentPage
{
	public VehicleDetailPage()
	{
		InitializeComponent();
        BindingContext = new Vehicle_DetailsViewModel();
    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}