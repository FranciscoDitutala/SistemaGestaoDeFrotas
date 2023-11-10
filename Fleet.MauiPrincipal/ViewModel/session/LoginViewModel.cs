
using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using Microsoft.Maui.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.session
{
    public partial class LoginViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        [ObservableProperty]
        public string _NameUser;
        [ObservableProperty]
        public string _PassUser;
        public LoginViewModel()
        {

            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
           
        }
        public string token;

        public ICommand EntrarSistemaCommand => new Command(async () =>
         await EntrarSistemaAsync());

        private async Task EntrarSistemaAsync()
        {
            //Debug.WriteLine("Entrou no metodo Login ");
            var Login = new User
            {
                Password = PassUser,
                UserName = NameUser
            };
            var url = $"{baseUrl}/api/Authenticate/login";
            string json = JsonSerializer.Serialize<User>(Login, _SerializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<JsonDocument>(conteudo);
                token = tokenResponse.RootElement.GetProperty("token").GetString();
                
                await Application.Current.MainPage.DisplayAlert("Informação ", "Login com sucesso!", "Ok");
            
                await Application.Current.MainPage.Navigation.PushAsync(new AppShell(token));
                //await Application.Current.MainPage.Navigation.PushAsync(new AppShell());
                NameUser = "";
                PassUser = "";
            }
            else
            {
                {
                    // Lidere com erros de autenticação
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Usuario não encontrado", "Ok");

                }

            }


            ////var user = Convert.ToString(UserName.Text);
            ////var pass = Convert.ToString(PassUser.Text);
            //if (((!string.IsNullOrEmpty(NameUser)) && (!string.IsNullOrEmpty(PassUser))))
            //{
            //    if (NameUser.Equals("admin") && PassUser.Equals("1234"))
            //    {
            //        await Application.Current.MainPage.DisplayAlert("Informação", "Login com sucesso", "Ok");
            //        NameUser = "";
            //        PassUser = "";
            //        await Application.Current.MainPage.Navigation.PushAsync(new AppShell());
            //    }
            //    else
            //    {
            //      await    Application.Current.MainPage.DisplayAlert("Atenção", "Usuario não encontrado", "Ok");
            //    }
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Atenção", "Campos Obrigatórios em brancos", "Ok");

            //}
        }
    }
}
