

using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoryPage : ContentPage
{
	public PartCategoryPage(PartCategoryViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}