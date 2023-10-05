namespace Fleet.Management.View;

public partial class VehicleBrandsPage : ContentPage
{
    public VehicleBrandsPage(VehicleBrandsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}