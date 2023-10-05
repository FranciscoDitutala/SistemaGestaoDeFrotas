using System.ComponentModel;

namespace Fleet.Management.ViewModel;

public abstract partial class BaseViewModel<T> : NotifyDataErrorInfo<T> where T : BaseViewModel<T>
{
    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotChanged))]
    bool isChanged;

    [ObservableProperty]
    string title;

    public bool IsNotChanged => !IsChanged;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if(e.PropertyName != nameof(IsChanged) &&
            e.PropertyName != nameof(IsRefreshing) &&
            e.PropertyName != nameof(Title) && !updatedProperties.Add(e.PropertyName))
            IsChanged = true;
    }

    private HashSet<string> updatedProperties = new();

    public async Task<bool> ShowErrors()
    {
        if (HasErrors)
        {
            string errorList = "Revise o Seguinte:";
            foreach (var error in GetErrors())
            {
                errorList += $"\n - {error}";
            }
            await Shell.Current.DisplayAlert("Erros de Edição!", errorList, "OK");

            return true;
        }

        return false;
    }
}

