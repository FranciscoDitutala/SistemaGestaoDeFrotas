using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
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
    public partial class UsersDetailsViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        public User userVerify;

        public UsersDetailsViewModel(User userParam)
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            userVerify = userParam;

            CarregarUsersAsync();



        }


        private User _users;
        public User Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        //public ICommand CarregarUsersCommand => new Command(async () =>
        // await CarregarVehiclesAsync());
        private async Task CarregarUsersAsync()
        {
            Users = new User();
            var url = $"{baseUrl}/api/Authenticate/get-user/{userVerify.UserName}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<User>
                        (responseStream, _SerializerOptions);

                    Users = data;

                }
        }

        //public ICommand GoToUpdateVehicleCommand => new Command(async () =>
        //     await GoToUpdateVehicleAsync());
        //private async Task GoToUpdateVehicleAsync()
        //{
        //    await Application.Current.MainPage.Navigation.PushAsync(new VehicleAddPage(Vehicles));
        //}
        public ICommand VoltarCommand => new Command(async () =>
          await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public ICommand RemoveSelectedCommand => new Command(async () =>
             await DeletarUsersAsync());
        private async Task DeletarUsersAsync()
        {
            var option = await Application.Current.MainPage.DisplayActionSheet("Pretende Apagar o Usuario? ", "Cancelar,", "Deletar");
            if (option.Equals("Deletar"))
            {
                var url = $"{baseUrl}/api/Authenticate/delete-user";
                var response = await Client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Atencao ", "O Usuario deletado com sucesso ", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atencao ", "O Usuario não deletado ", "OK");
                  
                }
                //Debug.WriteLine("O usuario foi apagado com id " + userVerify.Id);
                
            }

        }
    }
}
