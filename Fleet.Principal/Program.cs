using Fleet.Common;
using Fleet.Parts;
using Fleet.Principal.Data;
using Fleet.Principal.Infrastructure;
using Fleet.Principal.Services.CommonServices;
using Fleet.Principal.Services.CommonServices.Interfaces;
using Fleet.Principal.Services.PartsServices;
using Fleet.Principal.Services.PartsServices.Interfaces;
using Fleet.Principal.Services.TransportServices;
using Fleet.Principal.Services.TransportServices.Interfaces;
using Fleet.Transport;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

//string CommonBaseAddress = "http://127.0.0.1:5107"; //TODO: Get from Configuration in production
//string PartsBaseAddress = "http://127.0.0.1:5124"; //TODO: Get from Configuration in production

//string CommonBaseAddress = "http://172.16.33.154:5107"; //TODO: Get from Configuration in production
//string PartsBaseAddress = "http://172.16.33.154:5124"; //TODO: Get from Configuration in production
//string TransportBaseAddress = "http://172.16.33.154:5125";

string CommonBaseAddress = "http://127.0.0.1:5107"; //TODO: Get from Configuration in production
string PartsBaseAddress = "http://127.0.0.1:5124"; //TODO: Get from Configuration in production
string TransportBaseAddress = "http://127.0.0.1:5125";
var builder = WebApplication.CreateBuilder(args);


ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("FleetPrincipalDbConnection")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})



// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(FleetPrincipalProfile));

var commonBaseUri = CommonBaseAddress;
var commonChanel = GrpcChannel.ForAddress(commonBaseUri);
var partsBaseUri = PartsBaseAddress;
var partsChanel = GrpcChannel.ForAddress(partsBaseUri);
var tranportBaseUri = TransportBaseAddress;
var transportChanel = GrpcChannel.ForAddress(tranportBaseUri);

//builder.Services.AddSingleton(Connectivity.Current);
//builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);


builder.Services.AddSingleton(services => new VehicleBrandManager.VehicleBrandManagerClient(commonChanel));
builder.Services.AddSingleton(services => new VehicleModelManager.VehicleModelManagerClient(commonChanel));
builder.Services.AddSingleton(services => new VehicleVariantManager.VehicleVariantManagerClient(commonChanel));
builder.Services.AddSingleton(services => new PartManager.PartManagerClient(partsChanel));
builder.Services.AddSingleton(services => new StockEntryManager.StockEntryManagerClient(partsChanel));
builder.Services.AddSingleton(services => new StockOutManager.StockOutManagerClient(partsChanel));
builder.Services.AddSingleton(services => new VehicleManager.VehicleManagerClient(transportChanel));
builder.Services.AddSingleton(services => new DocumentManager.DocumentManagerClient(transportChanel));
builder.Services.AddSingleton(services => new OrgaoManager.OrgaoManagerClient(transportChanel));
builder.Services.AddSingleton(services => new EmployeeManager.EmployeeManagerClient(transportChanel));
builder.Services.AddSingleton(services => new AssignmentManager.AssignmentManagerClient(transportChanel));


builder.Services.AddSingleton<IVehicleModelService, VehicleModelService>();
builder.Services.AddSingleton<IVehicleBrandService, VehicleBrandService>();
builder.Services.AddSingleton<IVehicleVariantService, VehicleVariantService>();
builder.Services.AddSingleton<IPartService,PartService>();
builder.Services.AddSingleton<IStockEntryService, StockEntryService>();
builder.Services.AddSingleton<IStockOutService, StockOutService>();
builder.Services.AddSingleton<IDocumentService, DocumentService>();
builder.Services.AddSingleton<IVehicleService, VehicleServices>();
builder.Services.AddSingleton<IOrgaoService,OrgaoService>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IAssignmentService, AssignmentService>();
builder.Services.AddSingleton<VehicleDetailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// adicionado
app.UseAuthentication();

app.MapControllers();

app.Run();
