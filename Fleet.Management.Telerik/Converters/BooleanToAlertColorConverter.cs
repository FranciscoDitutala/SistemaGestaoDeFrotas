using System.Globalization;
namespace Fleet.Management.Converters;

public class BooleanToAlertColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {


        return (value is bool positive) && !positive ? Colors.Red : Colors.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
