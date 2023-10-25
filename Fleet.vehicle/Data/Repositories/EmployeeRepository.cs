using Fleet.Transport.Data.Entities;
using System.Collections.ObjectModel;

namespace Fleet.Transport.Data.Repositories
{
    public class EmployeeRepository
    {
        public ObservableCollection<Employee> Employees{ get; set; } = new ObservableCollection<Employee>();


        public EmployeeRepository()
        {
            Employees.Add(new Employee { Id=1, Name="Gilmar Gaspar David",Nif=20201089});
            Employees.Add(new Employee { Id = 2, Name = "João Silva", Nif = 20304050 });
            Employees.Add(new Employee { Id = 3, Name = "Maria Santos", Nif = 30405060 });
            Employees.Add(new Employee { Id = 4, Name = "Pedro Ferreira", Nif = 40506070 });
            Employees.Add(new Employee { Id = 5, Name = "Ana Rodrigues", Nif = 50607080 });
            Employees.Add(new Employee { Id = 6, Name = "Carlos Mendes", Nif = 60708090 });
            Employees.Add(new Employee { Id = 7, Name = "Marta Pereira", Nif = 70809010 });
            Employees.Add(new Employee { Id = 8, Name = "Rui Costa", Nif = 80901011 });
            Employees.Add(new Employee { Id = 9, Name = "Sofia Fernandes", Nif = 91011121 });
            Employees.Add(new Employee { Id = 10, Name = "Hugo Almeida", Nif = 10111213 });
            Employees.Add(new Employee { Id = 11, Name = "Catarina Sousa", Nif = 11121314 });
            Employees.Add(new Employee { Id = 12, Name = "Luis Carvalho", Nif = 12131415 });
            Employees.Add(new Employee { Id = 13, Name = "Isabel Costa", Nif = 13141516 });
            Employees.Add(new Employee { Id = 14, Name = "António Oliveira", Nif = 14151617 });
            Employees.Add(new Employee { Id = 15, Name = "Teresa Martins", Nif = 15161718 });
            Employees.Add(new Employee { Id = 16, Name = "Paulo Santos", Nif = 16171819 });
            Employees.Add(new Employee { Id = 17, Name = "Rita Ferreira", Nif = 17181920 });
            Employees.Add(new Employee { Id = 18, Name = "Fernando Gomes", Nif = 18192021 });
            Employees.Add(new Employee { Id = 19, Name = "Alice Lima", Nif = 19202122 });
            Employees.Add(new Employee { Id = 20, Name = "Manuel Sousa", Nif = 20212223 });
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
