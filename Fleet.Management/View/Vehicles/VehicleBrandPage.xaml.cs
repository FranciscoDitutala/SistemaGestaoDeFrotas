namespace Fleet.Management.View;

public partial class VehicleBrandPage : ContentPage
{
	public VehicleBrandPage(VehicleBrandViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}