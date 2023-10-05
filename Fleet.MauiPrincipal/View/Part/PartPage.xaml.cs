using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartPage : Shell
{
	public PartPage(PartPageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}