using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleDetailPage : ContentPage
{
    public VehicleDetailPage()
    {
        InitializeComponent();
      
    }
    public VehicleDetailPage(int Id)
	{
		InitializeComponent();
        BindingContext = new Vehicle_DetailsViewModel(Id);
    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void GoToAddVehicle(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehicleAddPage());
    }
}