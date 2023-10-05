using Fleet.MauiPrincipal.ViewModel.session;
namespace Fleet.MauiPrincipal.View.session;


public partial class CreateUser : ContentPage
{
    private CreateUserViewModel _viewModel;
    public CreateUser(CreateUserViewModel viewModel)
	{
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;

    }
}