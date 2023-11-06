using Fleet.Transport.Data.Entities;
using System.Collections.ObjectModel;

namespace Fleet.Transport.Data.Repositories
{
    public class EmployeeRepository
    {
        public ObservableCollection<Employee> Employees{ get; set; } = new ObservableCollection<Employee>();


        public EmployeeRepository()
        {
            Employees.Add(new Employee { Id=1, Name="Gilmar Gaspar David",Nif=20201089, OrgaoId= 1});
            Employees.Add(new Employee { Id = 2, Name = "João Silva", Nif = 20304050 , OrgaoId = 1 });
            Employees.Add(new Employee { Id = 3, Name = "Maria Santos", Nif = 30405060 ,OrgaoId = 1 });
            Employees.Add(new Employee { Id = 4, Name = "Pedro Ferreira", Nif = 40506070, OrgaoId = 1 });
            Employees.Add(new Employee { Id = 5, Name = "Ana Rodrigues", Nif = 50607080 ,OrgaoId = 1 });
            Employees.Add(new Employee { Id = 6, Name = "Carlos Mendes", Nif = 60708090, OrgaoId = 2});
            Employees.Add(new Employee { Id = 7, Name = "Marta Pereira", Nif = 70809010, OrgaoId = 2});
            Employees.Add(new Employee { Id = 8, Name = "Rui Costa", Nif = 80901011, OrgaoId = 2 });
            Employees.Add(new Employee { Id = 9, Name = "Sofia Fernandes", Nif = 91011121, OrgaoId = 2 });
            Employees.Add(new Employee { Id = 10, Name = "Hugo Almeida", Nif = 10111213, OrgaoId = 3 });
            Employees.Add(new Employee { Id = 11, Name = "Catarina Sousa", Nif = 11121314 , OrgaoId = 3 });
            Employees.Add(new Employee { Id = 12, Name = "Luis Carvalho", Nif = 12131415 , OrgaoId = 3 });
            Employees.Add(new Employee { Id = 13, Name = "Isabel Costa", Nif = 13141516, OrgaoId = 4});
            Employees.Add(new Employee { Id = 14, Name = "António Oliveira", Nif = 14151617, OrgaoId = 4 });
            Employees.Add(new Employee { Id = 15, Name = "Teresa Martins", Nif = 15161718,OrgaoId=5 });
            Employees.Add(new Employee { Id = 16, Name = "Paulo Santos", Nif = 16171819 , OrgaoId = 6 });
            Employees.Add(new Employee { Id = 17, Name = "Rita Ferreira", Nif = 17181920 , OrgaoId = 7});
            Employees.Add(new Employee { Id = 18, Name = "Fernando Gomes", Nif = 18192021 , OrgaoId = 8});
            Employees.Add(new Employee { Id = 19, Name = "Alice Lima", Nif = 19202122  ,OrgaoId = 8 });
            Employees.Add(new Employee { Id = 20, Name = "Manuel Sousa", Nif = 20212223 , OrgaoId = 9 });
        }
        public Employee Find(int id)
        {
            foreach (Employee e in Employees)
            {
                if (e.Id == id)
                    return e;
            }
            return new Employee();
        }
    }
}
