using Fleet.MauiPrincipal.Service;
using Fleet.MauiPrincipal.ViewModel;

namespace Fleet.MauiPrincipal;

public partial class NewPage2 : ContentPage
{
	public NewPage2(string Vin, string Cor, string Transmission, string TypeVehicle, string Brand, string  Registartion )
	{
		InitializeComponent();
		/* NewPage2(Vin, Cor,_selectedTransmission,_selectedVehicleType, _selectedMarca.Name, Registration) */
		 BindingContext = new VehicleAddSecondViewModel(Vin, Cor, Transmission, TypeVehicle, Brand, Registartion  );
	}

	  private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    } 
    
}