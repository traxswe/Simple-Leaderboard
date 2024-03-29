﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Trax.Leaderboard
{
    public class ItalicConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return FontStyles.Normal;

            var isItalic = (bool)value;
            if (isItalic)
                return FontStyles.Italic;

            return FontStyles.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
