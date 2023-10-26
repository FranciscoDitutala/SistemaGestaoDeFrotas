using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.session
{
    public partial class LoginViewModel:ObservableObject

    {
        [ObservableProperty]
        public string _NameUser;
        [ObservableProperty]
        public string _PassUser;
        public LoginViewModel()
        {

        }

        
        public ICommand EntrarSistemaCommand => new Command(async () =>
         await EntrarSistemaAsync());

        private async Task EntrarSistemaAsync()
        {
            //var user = Convert.ToString(UserName.Text);
            //var pass = Convert.ToString(PassUser.Text);
            if (((!string.IsNullOrEmpty(NameUser)) && (!string.IsNullOrEmpty(PassUser))))
            {
                if (NameUser.Equals("admin") && PassUser.Equals("1234"))
                {
                    await Application.Current.MainPage.DisplayAlert("Informação", "Login com sucesso", "Ok");
                    NameUser = "";
                    PassUser = "";
                    //await AppShell.Current.GoToAsync(nameof(AppShell));
                    Application.Current.MainPage.Navigation.PushAsync(new AppShell());
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Atenção", "Usuario não encontrado", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atenção", "Campos Obrigatórios em brancos", "Ok");

            }
        }
    }
}
