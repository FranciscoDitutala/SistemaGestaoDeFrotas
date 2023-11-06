using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartPage: ContentPage
{
	public PartPage( )
	{
		InitializeComponent();
		BindingContext = new PartPageViewModel();

    }

    private async void GEntries(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StockyEntriesPage());
    }
    private async void GoToPart(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PartCategoriesPage());
    }
    private async void GoToOuts(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StockyOutPage());
    }
}