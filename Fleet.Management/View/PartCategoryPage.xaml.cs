namespace Fleet.Management.View;

public partial class PartCategoryPage : ContentPage
{
	public PartCategoryPage(PartCategoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}