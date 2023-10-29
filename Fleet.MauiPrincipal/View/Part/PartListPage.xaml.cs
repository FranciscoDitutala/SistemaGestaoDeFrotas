using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartListPage : ContentPage
{
	public PartListPage()
	{
		InitializeComponent();
		BindingContext = new PartListPageViewModelcs();

    }
    private async void GoToPartAddPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PartAddPage());
    }
    private async void GoToCategories(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PartCategoriesPage());
       
    }
}