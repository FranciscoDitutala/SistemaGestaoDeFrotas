namespace Fleet.Management
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VehicleBrandPage), typeof(VehicleBrandPage));
            Routing.RegisterRoute(nameof(VehicleModelPage), typeof(VehicleModelPage));
            Routing.RegisterRoute(nameof(VehicleVariantPage), typeof(VehicleVariantPage));

            Routing.RegisterRoute(nameof(PartCategoryPage), typeof(PartCategoryPage));
            //Routing.RegisterRoute(nameof(PartTypePage), typeof(PartTypePage));
            //Routing.RegisterRoute(nameof(PartPage), typeof(PartPage));
            //Routing.RegisterRoute(nameof(StockEntryPage), typeof(StockEntryPage));
            //Routing.RegisterRoute(nameof(StockOutPage), typeof(StockOutPage));
        }
    }
}