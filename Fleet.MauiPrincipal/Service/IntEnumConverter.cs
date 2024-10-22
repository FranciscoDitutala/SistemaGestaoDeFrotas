﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.MauiPrincipal.Service
{
    public class IntEnumConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return (int)value;
            }
            return 0;
        }
  
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                return Enum.ToObject(targetType, value);
            }
            return 0;
        }
    }
}
