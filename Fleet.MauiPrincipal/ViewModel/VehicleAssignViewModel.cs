using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.View;
using Fleet.MauiPrincipal.Service.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using Fleet.MauiPrincipal.View.Vehicle;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleAssignViewModel : ObservableObject
    {

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        public int typeAssign;
        private Vehicle selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                if (SelectedVehicle != value)
                {
                    selectedVehicle = value;
                    OnPropertyChanged(nameof(SelectedVehicle));
                }

            }
        }

        private List<Vehicle> _vehicles;
        public List<Vehicle> Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        [ObservableProperty]
        public int _TipoAtribuir;
        [ObservableProperty]
        public string _Descricao;

        public ObservableCollection<Vehicle> AtribuirItems { get; } = new();

        private List<Assignment> _atribuicao;
        public List<Assignment> Atribuicoes
        {
            get { return _atribuicao; }
            set
            {
                _atribuicao = value;
                OnPropertyChanged(nameof(Atribuicoes));
            }
        }
        public Vehicle vehicleParameter { get; set; }
        public Vehicle vehicle { get; set; }

        public VehicleAssignViewModel(Vehicle vehicle)
        {
            vehicleParameter = vehicle;
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehiclesAsync();
            CarregarEmployee();
            CarregarOrgao();
            CarregarAssignmentAsync();
           
        }


        public ICommand VoltarCommand => new Command(async () =>
await Voltar());
        private async Task Voltar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        //Metodo para carregar a Vehicle e anexar no Picker
        private async Task CarregarVehiclesAsync()
        {
            Vehicles = new List<Vehicle>();
            var url = $"{baseUrl}/FleetTransport/Vehicle";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Vehicle>>
                        (responseStream, _SerializerOptions);

                    Vehicles = data;
                }

        }

        // Metodo Carregar Empregado
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (SelectedEmployee != value)
                {
                    selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }

            }
        }

        private List<Employee> _employees;

        public List<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }
        private async Task CarregarEmployee()
        {
            Employees = new List<Employee>();
            var url = $"{baseUrl}/FleetTransport/Employee";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Employee>>
                        (responseStream, _SerializerOptions);

                    Employees = data;
                }
        }


        // Metodo Carregar Assigment Type

        private string _assign;
        public List<string> Assign
        {
            get
            {
                //return  Enum.GetNames(typeof(AssignmentType)).Select(b => b).ToList();
                return new List<String> { "Nenhum", "Role", "Funcionario", "Departamento" };
            }
        }

        private string _selectedAssignType;
        public string SelectedAssignType
        {
            get { return _selectedAssignType; }
            set
            {
                if (SelectedAssignType != value)
                {
                    _selectedAssignType = value;
                    OnPropertyChanged(nameof(SelectedAssignType));
                }
            }
        }


        // Metodo Carregar Orgão

        private Orgao _selectedOrgao;
        public Orgao SelectedOrgao
        {
            get { return _selectedOrgao; }
            set
            {
                if (SelectedOrgao != value)
                {
                    _selectedOrgao = value;
                    OnPropertyChanged(nameof(SelectedOrgao));
                }

            }
        }

        private List<Orgao> _orgao;
        public List<Orgao> Orgao
        {
            get { return _orgao; }
            set
            {
                _orgao = value;
                OnPropertyChanged(nameof(Orgao));
            }
        }
        private async Task CarregarOrgao()
        {
            Orgao = new List<Orgao>();
            var url = $"{baseUrl}/FleetTransport/Orgao";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Orgao>>
                        (responseStream, _SerializerOptions);

                    Orgao = data;
                }
        }

        // Cadastrar Atribuição

        public ICommand CadastrarAssignmentCommand => new Command(async () =>
       await CadastrarAssignmentAsync()

           );

        public async Task CadastrarAssignmentAsync()
        {
            Debug.WriteLine("Entro no metodo cadastrar Atribuição");
            var url = $"{baseUrl}/FleetTransport/Assignment";
            if (selectedVehicle.Id > 0)
            {
                var Atribuir = new Assignment
                {
                    Type = SelectedAssignType,
                    OrgaoId = _selectedOrgao.Id,
                    Description = Descricao,
                    EmployeeId = selectedEmployee.Id,
                    VehicleId = selectedVehicle.Id

                };
                Debug.WriteLine("Entrou no if");
                string json = JsonSerializer.Serialize<Assignment>(Atribuir, _SerializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Atribucao feita com sucesso ", "OK");
                    await Application.Current.MainPage.Navigation.PushAsync(new VehicleDetailPage(selectedVehicle.Id));
                    teste = Atribuir.Id;
                }
                else { await Application.Current.MainPage.DisplayAlert("Falhou", "Atribucao não feita  ", "OK"); }

            }

        }
        public int teste { get; set; }
        public ICommand CarregarAssignmentCommand => new Command(async () =>
            await CarregarAssignmentAsync());
        private async Task CarregarAssignmentAsync()
        {
            Atribuicoes = new List<Assignment>();
            var url = $"{baseUrl}/FleetTransport/Assignment/1";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Assignment>>
                        (responseStream, _SerializerOptions);
                    Atribuicoes = data;
                    Debug.WriteLine("Carregado com sucesso a lista de atribuiçoes " + Atribuicoes);
                }

        }
        public ICommand DeletarAssignmentCommand => new Command(async () =>
           await DeletarAssignmentAsync());
        private async Task DeletarAssignmentAsync()
        {
            await Application.Current.MainPage.DisplayAlert("Atenção", "Atribucao feita com sucesso ", "OK");
        }
    }
}

