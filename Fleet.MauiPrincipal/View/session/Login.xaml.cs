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
        var user = Convert.ToString(UserName.Text);
        var pass = Convert.ToString(PassUser.Text);
        if (((!string.IsNullOrEmpty(user)) && (!string.IsNullOrEmpty(pass))))
        {
            if (user.Equals("admin") && pass.Equals("1234"))
            {
                await DisplayAlert("Informação", "Login com sucesso", "Ok");
                UserName.Text = "";
                PassUser.Text = "";
                await Navigation.PushAsync(new AppShell());
            }
            else
            {
                await DisplayAlert("Atenção", "Usuario não encontrado", "Ok");
            }
        }
        else
        { 
            await DisplayAlert("Atenção", "Campos Obrigatórios em brancos", "Ok");

        }   
    }

    private async void RecuperarSenha(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RecuperarUser());
    }
    //protected override void OnSizeAllocated (double pageWidth, double pageHeight)
    //{
    //    base.OnSizeAllocated(pageWidth, pageHeight);
    //    const double aspectRatio = 1600 / 1441.0;
    //    BackImage.WidthRequest = Math.Max(pageHeight * aspectRatio, pageWidth);
    //}
}