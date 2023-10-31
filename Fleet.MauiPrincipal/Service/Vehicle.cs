using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service
{
    public class Vehicle
    {
        //public int Id { get; set; }
        //public string Vin { get; set; }
        //public string Brand { get; set; }
        //public string Model { get; set; }
        //public string Variant { get; set; }
        //public string Cor { get; set; }
        //public string Registration { get; set; }
        //public int YearOfManufacture { get; set; }
        //public int Type { get; set; }
        //public int Transmission { get; set; }
        //public int Power { get; set; }
        //public double FuelConsumption { get; set; }
        //public DateTime RegistrationDate { get; set; }

        public int Id { get; set; }
        public string? Vin { get; set; }
        public string? Registration { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public string? Cor { get; set; }

        [Range(1900, 2099)]
        public int YearOfManufacture { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Transmission { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Power { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]
        public double FuelConsumption { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        public bool Assigned { get; set; }

        //List<Document> Documents { get; set; } = new List<Document>();
        //List<Assignment> Assignments { get; set; } = new List<Assignment>();

    }

}
