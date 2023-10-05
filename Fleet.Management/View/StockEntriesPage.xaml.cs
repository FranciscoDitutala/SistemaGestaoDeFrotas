namespace Fleet.Management.View;

public partial class StockEntriesPage : ContentPage
{
	public StockEntriesPage(StockEntriesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}