using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service.Part
{
    public class StockEntry
    {
        public string ProvidersInfo { get; set; }
        public string Notes { get; set; }       
        public double TotalValue { get; set; }
        public List<StockEntryLines> Lines { get; set; } = new();
    }
}
