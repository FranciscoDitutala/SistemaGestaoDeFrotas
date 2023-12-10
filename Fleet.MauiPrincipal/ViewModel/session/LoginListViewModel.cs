using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.View.session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.session
{
    public partial  class LoginListViewModel: ObservableObject
    {

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
      
        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }
        public LoginListViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarUsersAsync();
        }
        public ObservableCollection<User> Items { get; set; }
        public ObservableCollection<User> SelectedItems { get; set; } = new ObservableCollection<User>();

        
        public ICommand GoToCreateUserCommand => new Command(async () =>
             await GoToCreateUserAsync());
        private async Task GoToCreateUserAsync()
        {
            User user = new User();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateUser(user));
        }
        public ICommand GoToUserDetailsCommand => new Command(async () =>
           await GoToUserDetailsAsync());
        private async Task GoToUserDetailsAsync()
        {
            Items = new ObservableCollection<User>(_users);
            foreach (var item in SelectedItems)
            {
                if (Items.Contains(item))
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new UserDetailPage(item));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Selecionar o Usuário", "Ok");
                }
            }
            
        }

        public ICommand CarregarUsersCommand => new Command(async () =>
             await CarregarUsersAsync());
        private async Task CarregarUsersAsync()
        {
            Users = new List<User>();
            var url = $"{baseUrl}/api/Authenticate/get-all-users";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<User>>
                        (responseStream, _SerializerOptions);

                    Users = data;
           
                }
        }

        [RelayCommand]
        public async void DisplayAlert(User user)
        {
            User UserSeleted = new User();
            if (Users != null && Users.Contains(user))
            {
                UserSeleted = user;
                var option = await Application.Current.MainPage.DisplayActionSheet("Selecionar a Opção", "Ok", null, "Atualizar", "Detalhes");
                if (option == "Atualizar")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CreateUser(UserSeleted));
                }
                else if (option == "Detalhes")
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new UserDetailPage(UserSeleted));
                }
            }
        }
    }
}
