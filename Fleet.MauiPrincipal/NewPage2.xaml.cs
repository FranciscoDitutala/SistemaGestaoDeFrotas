using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal;

public partial class NewPage2 : ContentPage
{
	public NewPage2(int Id, string Vin, string Cor, string Transmission, string TypeVehicle, string Brand, string  Registartion )
	{
		InitializeComponent();
		 BindingContext = new VehicleAddSecondViewModel(Id,Vin, Cor, Transmission, TypeVehicle, Brand, Registartion  );
	}
    public NewPage2(Vehicle vehicle)
    {
        InitializeComponent();
        BindingContext = new VehicleAddSecondViewModel(vehicle);
    }

    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    } 
    
}