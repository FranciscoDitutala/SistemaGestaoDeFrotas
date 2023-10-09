using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleModelPage : ContentPage
{
    private VehicleModelPageViewModel _viewModel;
	public VehicleModelPage(VehicleModelPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
    }
}