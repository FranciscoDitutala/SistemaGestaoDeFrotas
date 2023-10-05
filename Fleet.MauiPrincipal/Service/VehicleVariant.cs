using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service
{
    public class VehicleVariant
    {
        public int ReleaseYear { get; set; } 
        public int BodyStyle { get; set; } 
        public int NumDoors { get; set; } 
        public string Name { get; set; }
        public string TechnicalName { get; set; } 
        public string BodySpecs { get; set; }

    }
//    {
//  "releaseYear": 0,
//  "name": "string",
//  "technicalName": "string",
//  "bodySpecs": {
//    "bodyStyle": 0,
//    "numDoors": 0
//  },
//  "technicalSpecs": {
//    "fuelType": 0,
//    "consumptionKmL": 0,
//    "numCylinders": 0,
//    "cylinderArrangement": 0,
//    "transmissionType": 0,
//    "transmissionSpeeds": 0,
//    "drivertrainType": 0
//  },
//  "vehicleModelId": 0,
//  "enabled": true
//}
}
