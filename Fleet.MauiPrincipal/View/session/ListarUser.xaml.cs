using Fleet.MauiPrincipal.ViewModel.session;

namespace Fleet.MauiPrincipal.View.session;

public partial class ListarUser : ContentPage
{
	public ListarUser()
	{
		InitializeComponent();
		BindingContext = new LoginListViewModel();
	}
}