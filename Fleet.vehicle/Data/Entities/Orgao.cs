namespace Fleet.Transport.Data.Entities
{
    public class Orgao
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Acronym { get; set; }
       

       // List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        List<Assignment> Assignment { get; set; }= new List<Assignment>();
    }
}
