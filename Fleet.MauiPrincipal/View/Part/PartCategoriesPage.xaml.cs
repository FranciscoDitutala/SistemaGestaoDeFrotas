using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoriesPage : ContentPage
{
	private PartCategoriesViewModel _viewmodel;
	public PartCategoriesPage(PartCategoriesViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext = _viewmodel= viewmodel;
	}
}