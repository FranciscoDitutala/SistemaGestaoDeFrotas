using Fleet.MauiPrincipal.ViewModel;
namespace Fleet.MauiPrincipal.View.Vehicle;


public partial class VehiclePage : Shell
{
    public VehiclePage()
    {
        InitializeComponent();
        
    
    }
    protected override bool OnBackButtonPressed()
    {
        base.OnBackButtonPressed();
        return true;

    }
}