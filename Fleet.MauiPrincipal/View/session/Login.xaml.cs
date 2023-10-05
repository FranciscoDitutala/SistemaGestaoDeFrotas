using Fleet.MauiPrincipal.ViewModel.session;

namespace Fleet.MauiPrincipal.View.session;

public partial class Login : ContentPage
{
	private LoginViewModel _viewModel;

    public Login()
    {
        InitializeComponent();
    }

    public Login(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
    }

    private async void EntrarSistema(object sender, EventArgs e)
    {
        var user = Convert.ToString(UserName);
        var pass = Convert.ToString(PassUser);
        if (!(string.IsNullOrEmpty(user) && string.IsNullOrEmpty(pass))) {
             await Navigation.PushAsync(new AppShell());
        }
        else
        {
            await Shell.Current.DisplayAlert("", "Usuario ou senha invalida", "Ok");

        }
        

    }

    private async void RecuperarSenha(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RecuperarUser());

    }


}