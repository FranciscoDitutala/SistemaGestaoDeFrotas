using Fleet.Transport;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.TransPortDtos.VehicleDtos
{
    public class AddVehicleDto
    {
        [Required(ErrorMessage = "O campo Vin é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Vin deve ter no máximo 50 caracteres.")]
        public string? Vin { get; set; }
        [Required(ErrorMessage = "O campo Matricula é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O campo Matricula deve ter no máximo 50 caracteres.")]
        public string? Registration { get; set; }
        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Variant { get; set; }
        public string? Cor { get; set; }

        [Range(1900, 2100, ErrorMessage = "O ano deve estar entre 1900 a 2100.")]
        public int YearOfManufacture { get; set; }

        public string? Type { get; set; }

        public string ? Transmission { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A Quantidade deve ser um número inteiro positivo.")]
        public int Power { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "A Quantidade deve ser um número positivo.")]
        public double FuelConsumption { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool Assigned { get; set; }
    }
}
