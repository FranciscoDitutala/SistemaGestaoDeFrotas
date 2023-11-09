using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntriesPage : ContentPage
{
  
    public StockyEntriesPage()
    {
        InitializeComponent();
        this.BindingContext = new StockyEntriesPageViewModel();
    }
   
}