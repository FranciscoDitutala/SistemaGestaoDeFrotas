using System.ComponentModel.DataAnnotations;

namespace Fleet.Transport.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public int BrandId{ get; set; }

        public int ModelId { get; set; }

        public int VariantId { get; set; }

        [Range(1900, 2099)]
        public int  YearOfManufacture { get; set; }

        [Required] 
        public VehicleType Type  { get; set; }
       
        [Required]
        public TransmissionType Transmission  { get; set; }

        [Required]
        [Range(1, int.MaxValue)]  
        public int Power { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]
        public double FuelConsumption { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        public int OrgaoId { get; set; }
        public int EmployeeId { get; set; }
        public bool Assigned { get; set; }
        List<Document> Documents { get; set; }= new List<Document>();
        List<Assignment> Assignments { get; set; } = new List<Assignment>();

    }
}
