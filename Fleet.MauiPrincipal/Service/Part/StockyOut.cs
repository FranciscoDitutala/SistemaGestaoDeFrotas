﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service.Part
{
    public class StockyOut
    {
        public int Id { get; set; }
        public string RequestedBy { get; set; }
        public string Notes { get; set; }
        //public double TotalValue { get; set; }

        public ObservableCollection<RequestedLines> RequestedLines { get; set; } = new();
    }
}
