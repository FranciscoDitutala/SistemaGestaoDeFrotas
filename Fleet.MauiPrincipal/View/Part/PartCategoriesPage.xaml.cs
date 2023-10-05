using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoriesPage : ContentPage
{
	public PartCategoriesPage(PartCategoriesViewModel viemodel)
	{
		InitializeComponent();
		BindingContext = viemodel;
	}
}