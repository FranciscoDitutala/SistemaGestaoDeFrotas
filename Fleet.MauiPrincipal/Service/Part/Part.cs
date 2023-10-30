using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service.Part
{
   

    public class Part
    {
        public int Id { get; set; }
        public string Upc { get; set; }
        public string Sku { get; set; }
        public string PartTypeName { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        //public List<VehicleVariantFilter> VariantFilters { get; set; } = new();
        //public Variantfilter[] VariantFilters { get; set; }
    }

    //public class Variantfilter
    //{
    //    public int VehicleBrandId { get; set; }
    //    public int VehicleModelId { get; set; }
    //}

}
