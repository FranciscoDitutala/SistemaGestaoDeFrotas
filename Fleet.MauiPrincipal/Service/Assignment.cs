﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int VehicleId { get; set; }
        public int OrgaoId { get; set; }
        public int EmployeeId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfAssignment { get; set; }
        public DateTime EndDateOfAssignment {get; set;}
        //List<Document> Documents { get; set; } = new List<Document>();
    }
}
