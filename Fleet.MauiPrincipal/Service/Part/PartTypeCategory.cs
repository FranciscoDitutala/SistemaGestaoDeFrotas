using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service.Part
{
    public  class PartTypeCategory
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public string? SubCategory { get; set; }
        public string Image { get; set; }
    }

}
