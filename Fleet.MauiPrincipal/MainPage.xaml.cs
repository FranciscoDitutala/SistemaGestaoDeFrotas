using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            //Navigation.PushAsync(new AppShell());
        }

       
    }
}