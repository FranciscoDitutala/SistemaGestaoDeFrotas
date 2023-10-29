

using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoryPage : ContentPage
{
    public PartCategoryPage()
    {
        InitializeComponent(); 
    }
    public PartCategoryPage(string TypeName)
	{
		InitializeComponent();
        BindingContext = new PartCategoryViewModel(TypeName);
    }
}