using Fleet.MauiPrincipal.View;
using Fleet.MauiPrincipal.View.Part;
using Fleet.MauiPrincipal.View.session;
using Fleet.MauiPrincipal.View.Vehicle;
using System.Diagnostics;

namespace Fleet.MauiPrincipal
{
    public partial class AppShell : FlyoutPage
    {

        public string Token; 
        public AppShell(string token)
        {
            Token = token;
            InitializeComponent(); 
            Routing.RegisterRoute(nameof(VehicleBrandPage), typeof(VehicleBrandPage));
            Routing.RegisterRoute(nameof(VehicleModelPage), typeof(VehicleModelPage));
            Routing.RegisterRoute(nameof(VehicleVaraintPage), typeof(VehicleVaraintPage));
            Routing.RegisterRoute(nameof(VehicleListPage), typeof(VehicleListPage));
            Routing.RegisterRoute(nameof(VehicleAddPage), typeof(VehicleAddPage));
            Routing.RegisterRoute(nameof(NavBarPage), typeof(NavBarPage));
            Routing.RegisterRoute(nameof(VehicleAssignPage), typeof(VehicleAssignPage));
            Routing.RegisterRoute(nameof(VehicleAddPage), typeof(VehicleAddPage));
            //Routing.RegisterRoute(nameof(PartTypePage), typeof(PartTypePage));
            Routing.RegisterRoute(nameof(PartPage), typeof(PartPage));
            Routing.RegisterRoute(nameof(StockyEntryPage), typeof(StockyEntryPage));
            Routing.RegisterRoute(nameof(StockyOutPage), typeof(StockyOutPage));
            Routing.RegisterRoute(nameof(VehiclePage), typeof(VehiclePage));
            Routing.RegisterRoute(nameof(Login), typeof(Login));
            Routing.RegisterRoute(nameof(CreateUser), typeof(CreateUser));
            Routing.RegisterRoute(nameof(RecuperarUser), typeof(RecuperarUser));
            Routing.RegisterRoute(nameof(ListarUser), typeof(ListarUser));
            Routing.RegisterRoute(nameof(StockyEntriesPage), typeof(StockyEntriesPage));
            Routing.RegisterRoute(nameof(StockyOutsPage), typeof(StockyOutsPage));
            Routing.RegisterRoute(nameof(VehicleDetailPage), typeof(VehicleDetailPage));
           
            flyoutPage.btn.Clicked += OpenSecondPageClicked;
            flyoutPage.btn2.Clicked += OpenSecondPageClicked2;
            //flyoutPage.btn3.Clicked += OpenSecondPageClicked2;
            
        }

        private void OpenSecondPageClicked(object sender, EventArgs e)
        {
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
                Detail = new NavigationPage(new PartPage());
        } 
        private void OpenSecondPageClicked2(object sender, EventArgs e)
        {
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
                Detail = new NavigationPage(new VehicleListPage(Token));
        }
        private void OpenSecondPage(object sender, EventArgs e)
        {
            if (!((IFlyoutPageController)this).ShouldShowSplitMode)
                IsPresented = false;
                Detail = new NavigationPage(new CreateUser());
        }


    }   
}