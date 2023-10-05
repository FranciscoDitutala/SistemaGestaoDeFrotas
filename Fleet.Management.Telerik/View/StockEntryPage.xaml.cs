namespace Fleet.Management.View;

public partial class StockEntryPage : ContentPage
{
	public StockEntryPage(StockEntryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        //dataForm.BindingContext= viewModel;
    }
}