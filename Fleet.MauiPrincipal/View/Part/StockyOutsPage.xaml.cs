using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyOutsPage : ContentPage
{
	public StockyOutsPage(StockyOutsPageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext= viewmodel;
	}
}