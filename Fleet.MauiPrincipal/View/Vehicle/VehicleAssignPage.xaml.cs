using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleAssignPage : ContentPage
{

    //public VehicleAssignPage()
    //{
    //    InitializeComponent();
    //}
    //private VehicleAssignViewModel _viewModel;
	public VehicleAssignPage()
	{
		InitializeComponent();
        this.BindingContext = new VehicleAssignViewModel();
    }
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new VehicleListPage());
        await Navigation.PopAsync();
    }

}
