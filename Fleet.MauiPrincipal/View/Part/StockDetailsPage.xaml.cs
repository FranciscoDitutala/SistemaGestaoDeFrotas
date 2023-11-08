using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockDetailsPage : ContentPage
{
	public StockDetailsPage(int stockId)
	{
		InitializeComponent();
		BindingContext = new StockDetailsViewModel(stockId);
	}
}