using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.ViewModel.session;

namespace Fleet.MauiPrincipal.View.session;

public partial class UserDetailPage : ContentPage
{
	public UserDetailPage(User users)
	{
		InitializeComponent();
		BindingContext = new UsersDetailsViewModel(users);
	}
}