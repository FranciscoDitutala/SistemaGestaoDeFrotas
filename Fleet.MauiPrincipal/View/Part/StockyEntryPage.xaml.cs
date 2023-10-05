using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntryPage : ContentPage
{
	public StockyEntryPage(StockyEntryPageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}