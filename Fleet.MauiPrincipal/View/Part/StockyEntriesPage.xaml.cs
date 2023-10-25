using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class StockyEntriesPage : ContentPage
{
    public StockyEntriesPage()
    {
        InitializeComponent();
    }

    private StockyEntriesPageViewModel _viewModel;
    public StockyEntriesPage(StockyEntriesPageViewModel viewmodel)
	{
		InitializeComponent();
		this.BindingContext =_viewModel = viewmodel;
	}

    private async void GoToAddStock(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StockyEntryPage());
    }
}