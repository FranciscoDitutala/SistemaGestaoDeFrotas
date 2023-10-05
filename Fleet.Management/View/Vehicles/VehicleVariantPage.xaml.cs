namespace Fleet.Management.View;

public partial class VehicleVariantPage : ContentPage
{
	public VehicleVariantPage(VehicleVariantViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}