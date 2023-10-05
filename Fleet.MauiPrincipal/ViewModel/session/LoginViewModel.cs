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
    public partial class LoginViewModel
    {
        public LoginViewModel()
        {

        }

        [ICommand]
        public async void EntrarSistema()
        {
            //await AppShell.Current.GoToAsync(nameof(AppShell));
            ////await Navigation.PushAsync(new AppShell());
        }
      }
}
