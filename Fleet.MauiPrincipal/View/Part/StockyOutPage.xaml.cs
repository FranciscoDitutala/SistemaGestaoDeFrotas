using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyOutPage : ContentPage
{
	public StockyOutPage(StockyOut Outs)
	{
		InitializeComponent();
		BindingContext = new StockyOutPageViewModel(Outs);
	}
}