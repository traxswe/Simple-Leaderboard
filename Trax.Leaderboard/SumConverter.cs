using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Trax.Leaderboard
{
    public class SumConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double _Sum = 0;
            if (values == null)
                return _Sum;
            foreach (var item in values)
            {
                double _Value;
                if (double.TryParse(item.ToString(), out _Value))
                    _Sum += _Value;
            }
            return _Sum.ToString();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
