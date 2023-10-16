using CommunityToolkit.Mvvm.ComponentModel;
using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.Service.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fleet.MauiPrincipal.ViewModel
{
    public partial class VehicleAssignViewModel: ObservableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        static Random random = new();

        private HttpClient Client;
        JsonSerializerOptions _SerializerOptions;
        string baseUrl = "https://localhost:7111";
        private Vehicle selectedVehicle;
        public Vehicle SelectedVehicle
        {
            get { return selectedVehicle; }
            set {
              if(SelectedVehicle != value)
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
            set { _vehicles = value;
                OnPropertyChanged(nameof(Vehicles));
            }
        }

        [ObservableProperty]
        public int _TipoAtribuir;
        [ObservableProperty]
        public string _Descricao;

        public ObservableCollection<Vehicle> AtribuirItems { get; } = new();

        private List<Assignment> _atribuir;
        public List<Assignment> Atribuicoes
        {
            get { return _atribuir; }
            set
            {
                _atribuir = value;
                OnPropertyChanged(nameof(Atribuicoes));
            }
        }

        public VehicleAssignViewModel()
        {
            Client = new HttpClient();
            _SerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CarregarVehiclesAsync();
            CarregarEmployee();
            CarregarOrgao();
            CarregarAssignmentAsync();
            //CadastrarAssignmentAsync();


        }

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
   
      
        private AssignmentType _assign;
        public List<string> Assign
        {
            get {return  Enum.GetNames(typeof(AssignmentType)).Select(b => b).ToList();
            }
        }
        
        private AssignmentType _selectedAssignType;
        public AssignmentType SelectedAssignType
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

        //public void DoSomethingAssign()
        //{
        //    switch (SelectedAssignType)
        //    {
        //        case "None":
        //            _assign = AssignmentType.NONE;
        //            break;
        //        case "Role":
        //            _assign = AssignmentType.NONE;
        //            break;
        //        case "Employee":
        //            _assign = AssignmentType.NONE;
        //            break;
        //        case "Support":
        //            _assign = AssignmentType.NONE;
        //            break;
        //            ...
        //    }

            //DoSomething(_assign);
        //}

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
            var url = $"{baseUrl}/FleetTransport/Orgao/1";

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var data = await JsonSerializer.DeserializeAsync<List<Orgao>>
                        (responseStream, _SerializerOptions);

                    Orgao = data;
                }
        }

        //public ICommand CadastrarAssignmentCommand => new Command(async () =>
        //await  CadastrarAssignmentAsync()

        //    );

        //    public async Task CadastrarAssignmentAsync() { 
        //        var url = $"{baseUrl}/FleetTransport/Assignment";
        //        if (!string.IsNullOrEmpty(Descricao))
        //        {
        //            var Atribuir = new Assignment
        //            {
        //                Type = Tipo,
        //                OrgaoId = Orgao,
        //                Description = Descricao,
        //                EmployeeId = selectedEmployee.Id,
        //                VehicleId = selectedVehicle.Id

        //            };
        //            Console.Write("Entrou no if");
        //            string json = JsonSerializer.Serialize<Assignment>(Atribuir, _SerializerOptions);
        //            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //            var response = await Client.PostAsync(url, content);
        //        //await AppShell.Current.DisplayAlert("Atenção", "Atribucao com sucesso ", "OK");
        //       }
        //    //else
        //    //{
        //    //    //await AppShell.Current.DisplayAlert("Atenção", "Atribucao Falhou", "OK");
        //}

        public ICommand CarregarAssignmentCommand => new Command(async () =>
            await CarregarAssignmentAsync());
        private async Task CarregarAssignmentAsync()
        {
            Atribuicoes = new List<Assignment>();
            if (TipoAtribuir > 0)
            {
                var url = $"{baseUrl}/FleetTransport/Assignment/2";

                var response = await Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer.DeserializeAsync<List<Assignment>>
                            (responseStream, _SerializerOptions);
                        Atribuicoes = data;
                    }
            }
        }
    }
}
