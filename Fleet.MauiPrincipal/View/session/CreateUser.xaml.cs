using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.ViewModel.session;
namespace Fleet.MauiPrincipal.View.session;


public partial class CreateUser : ContentPage
{
    
   
    public CreateUser(User user)
    {
        InitializeComponent();

        this.BindingContext = new CreateUserViewModel(user);

    }
}