using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntryPage : ContentPage
{
    //public StockyEntryPage()
    //{
    //    InitializeComponent();

    //}
    public StockyEntryPage()
	{
		InitializeComponent();
		this.BindingContext =new StockyEntryPageViewModel();
	}

    private async void GoToAddFleet(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new PartAddPage());
    }   
   private async void ReturnToListStockyEntry(object sender, EventArgs e)
    {
        //if(Navigation.NavigationStack.Count > 3)
        //{
            await Navigation.PushAsync(new StockyEntriesPage());
          
        //}
        //else
        //{
        //    await Navigation.PopAsync();
        //}
       
    }
}