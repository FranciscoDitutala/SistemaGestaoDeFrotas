using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyOutPage : ContentPage
{
	public StockyOutPage()
	{
		InitializeComponent();
		BindingContext = new StockyOutPageViewModel();
	}
}