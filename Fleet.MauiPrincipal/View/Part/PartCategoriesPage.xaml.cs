using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoriesPage : ContentPage
{

	public PartCategoriesPage()
	{
		InitializeComponent();
		BindingContext = new PartCategoriesViewModel();
	}

    //ReturnToListVehicle
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        //await Navigation.PushAsync(new PartListPage());
    }
    private async void GoToPartCategory(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PartCategoryPage());
    }
}