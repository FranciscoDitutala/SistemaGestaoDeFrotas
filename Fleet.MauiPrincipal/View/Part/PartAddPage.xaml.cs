using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartAddPage : ContentPage
{
    public PartAddPage()
    {
        InitializeComponent();
    }
    public PartAddPage(string upc, Categories categoria , int Quant )
    {
        InitializeComponent();
        this.BindingContext = new PartAddPageViewModel(upc, categoria, Quant);
    }
   
}