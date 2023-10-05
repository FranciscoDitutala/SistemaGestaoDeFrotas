using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleVaraintPage : ContentPage
{
    private VehicleVariantViewModel _viewModel;
    public VehicleVaraintPage(VehicleVariantViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;

    }

}