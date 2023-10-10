using Fleet.MauiPrincipal.ViewModel;
using Fleet.MauiPrincipal.Service;
using System.Collections.ObjectModel;

namespace Fleet.MauiPrincipal.View.Vehicle;

public partial class VehicleListPage : ContentPage
{
    private VehicleListPageViewModel _viewModel;

    public VehicleListPage(VehicleListPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = _viewModel;
     
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        {
            //_viewModel.GetVehicleListCommand.Execute(null);
        }
       
    }

    private async void GoToVehicleAddPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new VehicleAddPage());
       
    }
    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {

    }
//    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
//    {
//        //string previous = (e.PreviousSelection.FirstOrDefault() as Vehicle)?.Vin;
//        string current = (e.CurrentSelection.FirstOrDefault() as Vehicle?.Vin;
       
//}

}