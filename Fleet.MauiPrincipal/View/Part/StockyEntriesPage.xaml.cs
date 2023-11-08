using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntriesPage : ContentPage
{
    //public StockyEntriesPage()
    //{
    //    InitializeComponent();
    //}

    public StockyEntriesPage()
    {
        InitializeComponent();
        this.BindingContext = new StockyEntriesPageViewModel();
    }
    //private async void GoToAddStock(object sender, EventArgs e)
    //{ 
    //    await Navigation.PushAsync(new StockyEntryPage(v));
    //}
}