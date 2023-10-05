namespace Fleet.MauiPrincipal.View.session;

public partial class RecuperarUser : ContentPage
{
	public RecuperarUser()
	{
		InitializeComponent();
	}

    private async void GuardarSenha(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Atenção", "A sua Senha Foi Recuperada com Sucesso", "Ok");
    }

    private async void FazerLogin(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}