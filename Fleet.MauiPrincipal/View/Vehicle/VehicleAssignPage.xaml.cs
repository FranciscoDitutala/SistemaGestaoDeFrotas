
using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAssignPage : ContentPage
{

    public VehicleAssignPage()
    {
        InitializeComponent();
    }
   
    public VehicleAssignPage(Fleet.MauiPrincipal.Service.Vehicle vehicle)
	{ 
		InitializeComponent();
        this.BindingContext = new VehicleAssignViewModel(vehicle);
    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new VehicleListPage());
        await Navigation.PopAsync();
    }

}
