

using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoryPage : ContentPage
{
    public PartCategoryPage()
    {
        InitializeComponent(); 
    }
    public PartCategoryPage(Categories TypeName)
	{
		InitializeComponent();
        BindingContext = new PartCategoryViewModel(TypeName);
    }
}