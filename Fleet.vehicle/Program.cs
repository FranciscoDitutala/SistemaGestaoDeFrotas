using Fleet.Transport;
using Fleet.Transport.Data;
using Fleet.Transport.Data.Repositories;
using Fleet.Transport.Infrastructure;
using Fleet.Transport.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true;
    options.MaxReceiveMessageSize = 2 * 1024 * 1024; // 2 MB
    options.MaxSendMessageSize = 5 * 1024 * 1024; // 5 MB
});

builder.Services.AddGrpcReflection();

builder.Services.AddDbContext<FleetTransportDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("FleetTransportDbConnection"));
}, contextLifetime: ServiceLifetime.Transient);


builder.Services.AddAutoMapper(typeof(FleetTransportProfile));
builder.Services.AddScoped<VehicleRepository>();
builder.Services.AddScoped<DocumentRepository>();
builder.Services.AddScoped<OrgaoRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<AssignmentRepository>();

builder.Services.AddScoped<DocumentService>();



var app = builder.Build();
// Configure the HTTP request pipeline.
app.MapGrpcService<VehicleService>();
app.MapGrpcService<DocumentService>();
app.MapGrpcService<OrgaoService>();
app.MapGrpcService<EmployeeService>();
app.MapGrpcService<AssignmentService>();


if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

app.MapGet("/", 
    () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit:" 
    +" https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
