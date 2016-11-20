using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FanaticFirefly.Helpers
{
    public class StringToActionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "1":
                    return "MUSIC VOLUME UP";
                case "2":
                    return "MUSIC VOLUME DOWN";
                case "3":
                    return "NEXT TRACK";
                case "4":
                    return "PREVIOUS TRACK";
                case "5":
                    return "SHUFFLE TRACKS";
                default: return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
