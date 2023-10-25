using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntryPage : ContentPage
{
    private StockyEntryPageViewModel _viewModel;

    public StockyEntryPage()
    {
        InitializeComponent();

    }
    public StockyEntryPage(StockyEntryPageViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext =_viewModel= viewmodel;
	}

    private async void GoToAddFleet(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PartAddPage());
    }   
   private async void ReturnToListStockyEntry(object sender, EventArgs e)
    {
        if(Navigation.NavigationStack.Count > 3)
        {
            await Navigation.PushAsync(new StockyEntriesPage());
          
        }
        else
        {
            await Navigation.PopAsync();
        }
       
    }
}