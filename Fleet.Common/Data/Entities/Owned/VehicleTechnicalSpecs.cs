using Microsoft.EntityFrameworkCore;

namespace Fleet.Common.Data;

public class VehicleTechnicalSpecs
{
    public EngineFuelType FuelType { get; set; }
    [Precision(27, 9)]
    public decimal ConsumptionKmL { get; set; }
    public int NumCylinders { get; set; }
    public EngineCylinderArrangement CylinderArrangement { get; set; }
    public VehicleTransmissionType TransmissionType { get; set; }
    public int TransmissionSpeeds { get; set; }
    public VehicleDrivertrainType DrivertrainType { get; set; }
}

