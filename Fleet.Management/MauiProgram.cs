using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Fleet.Management.Infrastructure;

namespace Fleet.Management
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

             string CommonBaseAddress = "http://127.0.0.1:5107"; //TODO: Get from Configuration in production
            string PartsBaseAddress = "http://127.0.0.1:5124"; //TODO: Get from Configuration in production
           // string CommonBaseAddress = "http://172.16.33.84:5107"; //TODO: Get from Configuration in production
           // string PartsBaseAddress = "http://172.16.33.84:5124"; //TODO: Get from Configuration in production
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddAutoMapper(typeof(FleetManagementProfile));

            var commonBaseUri = CommonBaseAddress;
            var commonChanel = GrpcChannel.ForAddress(commonBaseUri);
            var partsBaseUri = PartsBaseAddress;
            var partsChanel = GrpcChannel.ForAddress(partsBaseUri);

            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);


            builder.Services.AddSingleton(services => new VehicleBrandManager.VehicleBrandManagerClient(commonChanel));
            builder.Services.AddSingleton(services => new VehicleModelManager.VehicleModelManagerClient(commonChanel));
            builder.Services.AddSingleton(services => new VehicleVariantManager.VehicleVariantManagerClient(commonChanel));
            builder.Services.AddSingleton(services => new PartManager.PartManagerClient(partsChanel));
            builder.Services.AddSingleton(services => new StockEntryManager.StockEntryManagerClient(partsChanel));

            builder.Services.AddSingleton<VehicleBrandsViewModel>();
            builder.Services.AddSingleton<VehicleBrandsPage>();

            builder.Services.AddTransient<VehicleBrandViewModel>();
            builder.Services.AddTransient<VehicleBrandPage>();

            builder.Services.AddTransient<VehicleModelViewModel>();
            builder.Services.AddTransient<VehicleModelPage>();

            builder.Services.AddTransient<VehicleVariantViewModel>();
            builder.Services.AddTransient<VehicleVariantPage>();


            builder.Services.AddSingleton<PartCategoriesViewModel>();
            builder.Services.AddSingleton<PartCategoriesPage>();

            builder.Services.AddTransient<PartCategoryViewModel>();
            builder.Services.AddTransient<PartCategoryPage>();

            //builder.Services.AddTransient<PartTypeViewModel>();
            //builder.Services.AddTransient<PartTypePage>();

            //builder.Services.AddTransient<PartViewModel>();
            //builder.Services.AddTransient<PartPage>();

            builder.Services.AddSingleton<StockEntriesViewModel>();
            builder.Services.AddSingleton<StockEntriesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}