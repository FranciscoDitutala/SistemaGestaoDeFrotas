using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service.Part
{
    public class StockEntry
    {
        public int Id { get; set; } 
        public string ProvidersInfo { get; set; }
        public string Notes { get; set; }       
        public double TotalValue { get; set; }
        public ObservableCollection<StockEntryLines> Lines { get; set; } = new();
        public DateTime BeginDate { get; set; }
        public DateTime LastDate { get; set; }
    }
}
