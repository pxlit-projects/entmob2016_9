using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.Helpers
{
    public class StringToCommandTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "1":
                    return "UP";
                case "2":
                    return "DOWN";
                case "3":
                    return "LEFT";
                case "4":
                    return "RIGHT";
                case "5":
                    return "SHAKE";
                default: return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
