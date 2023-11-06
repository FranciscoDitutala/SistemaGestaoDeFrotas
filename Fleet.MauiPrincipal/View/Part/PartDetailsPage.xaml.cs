using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartDetailsPage : ContentPage
{
	public PartDetailsPage()
	{
		InitializeComponent();
	}
    public PartDetailsPage(string typeName)
    {
        InitializeComponent();
        BindingContext = new PartDetailsPageViewModel(typeName);
    }
}