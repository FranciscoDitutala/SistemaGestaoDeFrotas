using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleBrandPage : ContentPage
{
	private VehicleBrandViewModel _viewModel;
	public VehicleBrandPage(VehicleBrandViewModel ViewModel)
	{
		InitializeComponent();
		_viewModel = ViewModel;
		this.BindingContext= _viewModel;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await DisplayAlert("Atenção", "Pretendes apagar a marca selecionada", "Cancelar" ,"OK");

    }
}