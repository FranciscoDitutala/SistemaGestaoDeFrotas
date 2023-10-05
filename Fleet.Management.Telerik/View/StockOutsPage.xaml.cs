namespace Fleet.Management.View;

public partial class StockOutsPage : ContentPage
{
    public StockOutsPage(StockOutsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
