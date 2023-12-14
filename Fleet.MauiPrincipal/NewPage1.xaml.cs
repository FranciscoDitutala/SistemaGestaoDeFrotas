using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
		BindingContext = new VehicleLocalizationViewModel();
	}
}