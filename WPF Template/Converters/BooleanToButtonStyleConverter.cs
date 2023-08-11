// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF_Template.Converters;

internal class BooleanToButtonStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        Style style = new();

        if(value is not Boolean IsAccent)
        {
            throw new ArgumentException("Exception BooleanToButtonStyleConverter");
        }

        if(IsAccent)
        {
            style = (Style)App.Current.FindResource("MahApps.Styles.Button.Square.Accent");
        }
        else
        {
            style = (Style)App.Current.FindResource("MahApps.Styles.Button.Square");
        }

        return style;

    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This method is not used for button style static resource converters.
        return null;
    }
}
