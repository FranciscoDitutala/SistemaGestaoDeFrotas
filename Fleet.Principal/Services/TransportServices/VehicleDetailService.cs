using Fleet.Principal.Dtos.TransPortDtos.VehicleDtos;
using Fleet.Principal.Services.TransportServices.Interfaces;

namespace Fleet.Principal.Services.TransportServices
{
    public class VehicleDetailService
    {

        private readonly IVehicleService _vehicleService;
        private readonly IAssignmentService _assignmentService;
        private readonly IOrgaoService _orgaoService;
        private readonly IEmployeeService _employeeService;
        public VehicleDetail vehicleDetail = new VehicleDetail();
        public VehicleDetailService(IVehicleService vehicleService, IAssignmentService assignmentService, 
            IOrgaoService orgaoService, IEmployeeService employeeService)
        {
            _vehicleService = vehicleService;
            _assignmentService = assignmentService;
            _orgaoService = orgaoService;
            _employeeService = employeeService;
        }


        public async Task<VehicleDetail> FindVehicleDetail(int vehicleId)
        {
            var vehicle = await _vehicleService.FindVehicleAsync(vehicleId);
            var assigment = await _assignmentService.FindAssignmentVehicleAsync(vehicleId);
            var orgao = await _orgaoService.FindOrgao(assigment.OrgaoId);
            var employee = await _employeeService.FindEmployee(assigment.EmployeeId);

            if (vehicle.Id <= 0) return new VehicleDetail();

            vehicleDetail.YearOfManufacture=vehicle.YearOfManufacture;
            vehicleDetail.Transmission= vehicle.Transmission;
            vehicleDetail.Variant = vehicle.Variant;
            vehicleDetail.Vin=vehicle.Vin;
            vehicleDetail.Registration=vehicle.Registration;
            vehicleDetail.RegistrationDate=vehicle.RegistrationDate;
            vehicleDetail.Brand=vehicle.Brand;
            vehicleDetail.Assigned=vehicle.Assigned;
            vehicleDetail.Cor=vehicle.Cor;
            vehicleDetail.Employee = employee.Name;
            vehicleDetail.Orgao = orgao.Name;
            vehicleDetail.Power = vehicle.Power;
            vehicleDetail.FuelConsumption=vehicle.FuelConsumption;
            vehicleDetail.TypeAssigment = assigment.Type;
            vehicleDetail.Type=vehicle.Type;
            vehicleDetail.Model=vehicle.Model;

            return vehicleDetail;
        }
    }
}
