namespace Fleet.Management.View;

public partial class PartPage : ContentPage
{
	public PartPage(PartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}