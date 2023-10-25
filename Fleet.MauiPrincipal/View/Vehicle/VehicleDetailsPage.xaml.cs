namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleDetailsPage : ContentPage
{
	public VehicleDetailsPage()
	{
		InitializeComponent();
	}

    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
  
}