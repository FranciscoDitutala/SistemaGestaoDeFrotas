using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartTypePage : ContentPage
{
	public PartTypePage(PartTypePageViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}