using Fleet.MauiPrincipal.View;
using Fleet.MauiPrincipal.View.Part;
using Fleet.MauiPrincipal.View.session;
using Fleet.MauiPrincipal.View.Vehicle;

namespace Fleet.MauiPrincipal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(VehicleBrandPage), typeof(VehicleBrandPage));
            Routing.RegisterRoute(nameof(VehicleModelPage), typeof(VehicleModelPage));
            Routing.RegisterRoute(nameof(VehicleVaraintPage), typeof(VehicleVaraintPage));
            Routing.RegisterRoute(nameof(VehicleListPage), typeof(VehicleListPage));
            Routing.RegisterRoute(nameof(VehicleAddPage), typeof(VehicleAddPage));
            Routing.RegisterRoute(nameof(NavBarPage), typeof(NavBarPage));
            Routing.RegisterRoute(nameof(VehicleAssignPage), typeof(VehicleAssignPage));

            //Routing.RegisterRoute(nameof(PartTypePage), typeof(PartTypePage));
            Routing.RegisterRoute(nameof(PartPage), typeof(PartPage));
            Routing.RegisterRoute(nameof(StockyEntryPage), typeof(StockyEntryPage));
            Routing.RegisterRoute(nameof(StockyOutPage), typeof(StockyOutPage));
            Routing.RegisterRoute(nameof(VehiclePage), typeof(VehiclePage));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(CreateUser), typeof(CreateUser));
            Routing.RegisterRoute(nameof(RecuperarUser), typeof(RecuperarUser));
            Routing.RegisterRoute(nameof(ListarUser), typeof(ListarUser));
          
        }
    }
}