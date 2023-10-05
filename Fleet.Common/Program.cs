using Fleet.Common.Infrastructure;
using Fleet.Common.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.MaxReceiveMessageSize = 2 * 1024 * 1024; // 2 MB
    options.MaxSendMessageSize = 5 * 1024 * 1024; // 5 MB
});
builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<FleetCommonDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("FleetCommonDbConnection"));
}, contextLifetime: ServiceLifetime.Transient);

builder.Services.AddAutoMapper(typeof(FleetCommonProfile));

builder.Services.AddScoped<VehicleBrandRepository>();
builder.Services.AddScoped<VehicleModelRepository>();
builder.Services.AddScoped<VehicleVariantRepository>();
builder.Services.AddScoped<VehicleModelService>();
builder.Services.AddScoped<VehicleVariantService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<VehicleBrandService>();
app.MapGrpcService<VehicleModelService>();
app.MapGrpcService<VehicleVariantService>();

if(app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

await DbInitializer.EnsurePopulated(app);

app.Run();
