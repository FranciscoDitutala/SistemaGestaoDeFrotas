namespace Fleet.Management.View;

public partial class VehicleModelPage : ContentPage
{
	public VehicleModelPage(VehicleModelViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}