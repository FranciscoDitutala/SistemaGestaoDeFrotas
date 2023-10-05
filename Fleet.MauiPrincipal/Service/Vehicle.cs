using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service
{
    public class Vehicle
    {       public int BrandId { get; set; }
            public int ModelId { get; set; }
            public int VariantId { get; set; }
            public int YearOfManufacture { get; set; }
            public int Type { get; set; }
            public int Transmission { get; set; }
            public int Power { get; set; }
            public int FuelConsumption { get; set; }
            public DateTime RegistrationDate { get; set; }
     
    }
}
