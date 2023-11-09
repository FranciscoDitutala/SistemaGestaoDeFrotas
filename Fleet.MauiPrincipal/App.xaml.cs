using Fleet.MauiPrincipal.View;
using Fleet.MauiPrincipal.View.session;
using Microsoft.Maui.Controls;


namespace Fleet.MauiPrincipal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

             //MainPage = new Login();
            MainPage = new NavigationPage(new Login());
            //MainPage = new AppShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            //const int newWidth = 1400;
            //const int newHeigth = 800;
            //window.X = 200;
            //window.Y = 200;
            //window.Width = newWidth;
            //window.Height = newHeigth;
            //window.MaximumWidth = newWidth;
            //window.MaximumHeight = newHeigth;
           
            //// Center the window
            //window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
            //window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;
            //window.MaximumWidth = 1400;
            //window.MaximumHeight = 900;
            return window;

        }
     

    }
}