using CommunityToolkit.Mvvm.Input;
using Fleet.MauiPrincipal.View.Part;

using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Fleet.MauiPrincipal.ViewModel.Parts
{
    public class Fruit
    {
        public string FruitName { get; set; }
        public string FruitDescription { get; set; }
    }

    public partial class PartCategoriesViewModel : ObservableObject
    {
      

    }
}
