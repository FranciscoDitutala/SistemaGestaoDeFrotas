namespace Fleet.Management.View;

public partial class StockOutPage : ContentPage
{
    public StockOutPage(StockOutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
