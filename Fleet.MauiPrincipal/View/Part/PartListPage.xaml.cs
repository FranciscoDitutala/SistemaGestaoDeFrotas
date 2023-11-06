using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartListPage : ContentPage
{
	public PartListPage()
	{
		InitializeComponent();
    }
    public PartListPage(PartTypeCategory TypeCategory)
    {
        InitializeComponent();
        BindingContext = new PartListPageViewModelcs(TypeCategory);

    }
    private async void GoToPartAddPage(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new PartAddPage());
    }
    private async void GoToCategories(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PartCategoriesPage());
       
    }
}