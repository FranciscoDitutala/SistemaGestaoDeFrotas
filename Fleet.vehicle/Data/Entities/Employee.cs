namespace Fleet.Transport.Data.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Nif { get; set; }

        //List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        List<Assignment> Assignment { get; set; } = new List<Assignment>();

    }
}
