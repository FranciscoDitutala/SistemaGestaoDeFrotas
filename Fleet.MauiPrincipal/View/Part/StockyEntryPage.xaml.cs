using Fleet.MauiPrincipal.ViewModel.Parts;
using Fleet.MauiPrincipal.Service.Part;
namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntryPage : ContentPage
{
    //public StockyEntryPage()
    //{
    //    InitializeComponent();
    //    //BindingContext = new StockyEntryPageViewModel();
    //}
    public StockyEntryPage(StockEntry stock)
	{
		InitializeComponent();
		BindingContext = new StockyEntryPageViewModel(stock);
	}
 
   //private async void ReturnToListStockyEntry(object sender, EventArgs e)
   // {
   //         await Navigation.PushAsync(new StockyEntriesPage());
    
       
   // }
}