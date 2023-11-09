using Fleet.MauiPrincipal.ViewModel.session;
namespace Fleet.MauiPrincipal.View.session;


public partial class CreateUser : ContentPage
{
    
    public CreateUser()
	{
        InitializeComponent();
  
        this.BindingContext = new CreateUserViewModel();

    }
}