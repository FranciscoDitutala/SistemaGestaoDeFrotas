using Fleet.MauiPrincipal.View;
using Fleet.MauiPrincipal.View.session;
using Fleet.MauiPrincipal.View.Part;
using Fleet.MauiPrincipal.View.Vehicle;
using Fleet.MauiPrincipal.ViewModel;
using Fleet.MauiPrincipal.ViewModel.session;
using Fleet.MauiPrincipal.ViewModel.Parts;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using UraniumUI;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Handlers.Compatibility;



namespace Fleet.MauiPrincipal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseMauiApp<App>()
                
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                    fonts.AddFontAwesomeIconFonts();
                });
            builder
            .UseMauiApp<App>()
            .ConfigureMauiHandlers(handlers =>
            {
                // Add following line:
                handlers.AddInputKitHandlers(); // 👈
                //handlers.AddCompatibilityRenderer(typeof(Shell), typeof(ShellRenderer));
            });
            
            //.ConfigureMauiHandlers((_, handlers) => handlers.AddCompatibilityRenderer(typeof(Shell), typeof(ShellRenderer)));


#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<VehicleBrandPage>();
            builder.Services.AddTransient<VehicleModelPage>();
            builder.Services.AddTransient<VehiclePage>();
            builder.Services.AddTransient<VehicleVaraintPage>();
            builder.Services.AddTransient<VehicleListPage>();
            builder.Services.AddTransient<VehicleAddPage>();
            builder.Services.AddTransient<NavBarPage>();
            builder.Services.AddTransient<Login>();
            builder.Services.AddTransient<CreateUser>();

            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<CreateUserViewModel>();
            builder.Services.AddTransient<RecuperarUser>();
            builder.Services.AddTransient<ListarUser>();

            builder.Services.AddTransient<PartAddPage>();
            builder.Services.AddTransient<PartListPage>();
            builder.Services.AddTransient<PartTypePage>();
            builder.Services.AddTransient<PartCategoriesPage>();
            builder.Services.AddTransient<PartCategoryPage>();
            builder.Services.AddTransient<PartPage>();
            builder.Services.AddTransient<StockyEntriesPage>();
            builder.Services.AddTransient<StockyEntryPage>();
            builder.Services.AddTransient<StockyOutPage>();
            builder.Services.AddTransient<StockyOutsPage>();

            builder.Services.AddTransient<PartCategoryViewModel>();
            builder.Services.AddTransient<PartCategoriesViewModel>();
            builder.Services.AddTransient<StockyOutsPageViewModel>();
            builder.Services.AddTransient<StockyOutPageViewModel>();
            builder.Services.AddTransient<PartTypePageViewModel>();
            builder.Services.AddTransient<StockyEntriesPageViewModel>();
            builder.Services.AddTransient<StockyEntryPageViewModel>();
           builder.Services.AddTransient<PartPageViewModel>();


            builder.Services.AddTransient<VehiclePageViewModel>();
            builder.Services.AddTransient<VehicleBrandViewModel>();
            builder.Services.AddTransient<VehicleVariantViewModel>();
            builder.Services.AddTransient<VehicleListPageViewModel>();
            builder.Services.AddTransient<VehicleAddPageViewModel>();
            builder.Services.AddTransient<NavBarPageViewModel>();
            builder.Services.AddTransient<VehicleVariantList>();
            builder.Services.AddTransient<VehicleVariantListViewModel>();
            builder.Services.AddTransient<VehicleModelPageViewModel>();
            builder.Services.AddTransient<VehicleAssignPage>();
            builder.Services.AddTransient<VehicleDetailsPage>();

            return builder.Build();
        }
    }
}