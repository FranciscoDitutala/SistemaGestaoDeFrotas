using Fleet.MauiPrincipal.ViewModel.Parts;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartAddFirstParge : ContentPage
{
	public PartAddFirstParge(string UpcPart, int Quant)
	{
		InitializeComponent();
		BindingContext = new PartAddFirstPargeViewModel(UpcPart, Quant);
	}
}