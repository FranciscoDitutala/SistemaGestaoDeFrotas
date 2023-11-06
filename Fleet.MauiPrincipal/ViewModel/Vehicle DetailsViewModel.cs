using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.View.Vehicle;
using Fleet.MauiPrincipal.Service;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class Vehicle_DetailsViewModel : ObservableObject
    {

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";

        [ObservableProperty]
        public string _estadoAtribuicao;
        [ObservableProperty]
        public string _funcionario;
        [ObservableProperty]
        public string _departamento;

        public int vehicleId;

        public Vehicle_DetailsViewModel(int Id)
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            vehicleId = Id;
            CarregarVehiclesAsync();
            CarregarAssignmentAsync();
            CarregarOrgao();
            CarregarEmployee();



        }

        public ObservableCollection<Vehicle> Items { get; set; }
        public ObservableCollection<Vehicle> SelectedItems { get; set; } = new ObservableCollection<Vehicle>();

        private Vehicle _vehicles;
        public Vehicle Vehicles
        {
            get { return _vehicles; }
            set
            {
                _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        public ICommand CarregarVehiclesCommand => new Command(async () =>
         await CarregarVehiclesAsync());
        private async Task CarregarVehiclesAsync()
        {
            Vehicles = new Vehicle();
            var url = $"{baseUrl}/FleetTransport/Vehicle/GetVehicleDetail/{vehicleId}";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<Vehicle>
                        (responseStream, _SerializerOptions);

                    Vehicles = data;
                    if (Vehicles.Assigned)
                    { EstadoAtribuicao = "Atribuido"; }
                    else { EstadoAtribuicao = "Não Atribuido"; }
                    Debug.WriteLine("A matricula do veiculo é " + Vehicles.Registration);
                }
        }

        public ICommand GoToUpdateVehicleCommand => new Command(async () =>
             await GoToUpdateVehicleAsync());
        private async Task GoToUpdateVehicleAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VehicleAddPage(Vehicles));
        }

        public ICommand GoToAssignVehicleCommand => new Command(async () =>
             await GoToAssignVehicleAsync());
        private async Task GoToAssignVehicleAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new VehicleAssignPage(Vehicles));
        }

        public ICommand RemoveSelectedCommand => new Command(async () =>
             await DeletarVehiclesAsync());
        private async Task DeletarVehiclesAsync()
        {
            var option = await Application.Current.MainPage.DisplayActionSheet("Pretende Apagar o veiculo? ", "Cancelar,", "Deletar");
            if (option.Equals("Deletar"))
            {
                var url = $"{baseUrl}/FleetTransport/Vehicle/{vehicleId}";
                var response = await Client.DeleteAsync(url);
                Debug.WriteLine("O veiculo foi apagado com id " + vehicleId);
                await Application.Current.MainPage.DisplayAlert("Atencao ", "O veiculo deletado com sucesso ", "OK");
                await Application.Current.MainPage.Navigation.PopAsync();
            }

        }

        private Assignment _atribuicao;
        public Assignment Atribuicoes
        {
            get { return _atribuicao; }
            set
            {
                _atribuicao = value;
                OnPropertyChanged(nameof(Atribuicoes));
            }
        }
        private async Task CarregarAssignmentAsync()
        {
            Atribuicoes = new Assignment();
            var url = $"{baseUrl}/FleetTransport/Assignment/GetAssignmentVehicle/{vehicleId}";
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<Assignment>
                        (responseStream, _SerializerOptions);
                    Atribuicoes = data;
                    Debug.WriteLine("Carregado com sucesso a  atribuiçao " + Atribuicoes);
                }
        }
        // Metodo Carregar Empregado

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
            var url1 = $"{baseUrl}/FleetTransport/Employee";

            var response = await Client.GetAsync(url1);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Employee>>
                        (responseStream, _SerializerOptions);

                    Employees = data;
                    foreach (var item in Employees)
                    {
                        if (item.Id == Atribuicoes.EmployeeId)
                        {
                            Funcionario = item.Name;
                        }

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
                    foreach (var item in Orgao)
                    {
                        if (item.Id == Atribuicoes.OrgaoId)
                        {
                            Departamento = item.Name;
                        }

                    }
                }
        }
        public ICommand RefreshCommand => new Command(async () =>
             await CarregarEmployee());
       
       }

}
