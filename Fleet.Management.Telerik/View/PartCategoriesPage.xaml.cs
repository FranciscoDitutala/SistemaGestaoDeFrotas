namespace Fleet.Management.View;

public partial class PartCategoriesPage : ContentPage
{
    public PartCategoriesPage(PartCategoriesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}