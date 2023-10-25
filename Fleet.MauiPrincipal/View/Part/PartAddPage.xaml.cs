using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartAddPage : ContentPage
{
	public PartAddPage()
	{
		InitializeComponent();
	}

    private PartAddPageViewModel _viewModel;
    public PartAddPage(PartAddPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = _viewModel = viewModel;
    }

    private async void ReturnToAddStock(object sender, EventArgs e)
    {
        //await AppShell.Current.GoToAsync(nameof(StockyEntryPage));
        //await Navigation.PopAsync();
        //  await Navigation.PopAsync();
        //await Navigation.PushAsync(StockyEntryPage());
        await Navigation.PushAsync(new StockyEntryPage());
    }
}