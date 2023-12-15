using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel.session
{
    public partial class CreateUserViewModel:ObservableObject
    {
        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        [ObservableProperty]
        public string _userName;
        [ObservableProperty]
        public string _password;
        [ObservableProperty]
        public string _emailEntry;
        
        public User userUpdate;
        public CreateUserViewModel(User user)
        { 
            userUpdate = user;
            Client = new HttpClient();
            //Vehicles = new ObservableCollection<Vehicle>();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            //FillTheFields();
            CarregarUserAsync();

        }
        public List<User> Users;
        public ICommand CarregarUserCommand => new Command(async () =>
           await CarregarUserAsync());
        private async Task CarregarUserAsync()
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

        public bool isNewItem;
        string path;
        int paramId;
        public void VerifyNewUser()
        {
            //foreach (var item in Users)
            //{

                //if (!item.Email.Equals(EmailEntry) && !item.UserName.Equals(UserName))
                //{
                    //Add
                    //isNewItem = true;
                    //if (isAdmin)
                    //{
                    //    path = "/api/Authenticate/register-admin";
                    //}
                    //else { path = "/api/Authenticate/register";  }
                  
                //}
                //else
                //{
                //    //Update
                //    isNewItem = false;
                //    if (!isAdmin)
                //    {
                //        path = $"/api/Authenticate/register-admin{item.Id}";
                //    }
                //    else
                //    {
                //        path = $"/api/Authenticate/edit-password{item.Id}";
                //    }
                   
                //    paramId = item.Id;
                //}

            //}
        }
        // Metodo Carregar user Type
        private string _userType;
        public List<string> UserType
        {
            get
            {      
                return new List<string> { "Usuario", "Administrador"};

            }
        }
        public ICommand VoltarCommand => new Command(async () =>
               await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private string _selectedUserType;
        public string SelectedUserType
        {
            get { return _selectedUserType; }
            set
            {
                if (SelectedUserType != value)
                {
                    _selectedUserType = value;
                    OnPropertyChanged(nameof(SelectedUserType));

                }
            }

        }
        public bool isAdmin;
        public void VerifyType()
        {
            if (SelectedUserType.Equals("Usuario"))
            {
                isAdmin = false;
                path = "/api/Authenticate/register";
            }
            else if (SelectedUserType.Equals("Administrador"))
            {
                isAdmin= true;
               
                path = "/api/Authenticate/register-admin";
            }
        }

        public ICommand CadastraUsuarioCommand => new Command(async () =>
          await CadastraUsuarioAsync());
        private async Task CadastraUsuarioAsync()
        {
            VerifyType();
            VerifyNewUser();
            var url = $"{baseUrl + path}";
    
                var user = new User
                {
                    UserName = UserName,
                    Password = Password,
                    Email = EmailEntry
                   
                };

                string json = JsonSerializer.Serialize<User>(user, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                
                    response = await Client.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        if (isAdmin)
                        {
                            await Application.Current.MainPage.DisplayAlert("Informação", "Admin Cadastrado com sucesso!", "Ok");
                        }
                        else { await Application.Current.MainPage.DisplayAlert("Informação", "Usuario Cadastrado com sucesso!", "Ok"); }
                   
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Falha", "Operação sem sucesso", "Ok");
                    }

            }

    }
}
