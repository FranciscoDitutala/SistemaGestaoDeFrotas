using Fleet.MauiPrincipal.Service.Part;
using Fleet.MauiPrincipal.ViewModel.Parts;
using System.Collections.Generic;
using UraniumUI.Material.Controls;

namespace Fleet.MauiPrincipal.View.Part;

public partial class PartCategoriesPage : ContentPage
{

	public PartCategoriesPage()
	{
		InitializeComponent();
		BindingContext = new PartCategoriesViewModel();
	}
    public string teste { get; set; }
    //ReturnToListVehicle
    private async void ReturnToListVehicle(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
  
    private async void Button_Clicked(object sender, EventArgs e)
    {
        //string tt = "Acessórios auto";

        //await Navigation.PushAsync(new PartCategoryPage(tt));
    }
}