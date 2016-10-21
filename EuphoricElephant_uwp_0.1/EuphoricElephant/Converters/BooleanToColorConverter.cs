using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace EuphoricElephant.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //var back = Application.Current.Resources["SystemChromeDisabledHighColor"];
            //Color defBack = Color.FromArgb(255, 215, 120, 0);

            if ((bool)value)
            {
                switch ((String)parameter)
                {
                    case "foreground":
                        return new SolidColorBrush(Colors.White);
                    case "background":
                        return new SolidColorBrush(Colors.DarkGray);
                }
            }
            else
            {
                switch ((String)parameter)
                {
                    case "foreground":
                        return new SolidColorBrush(Colors.Black);
                    case "background":
                        return new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                }
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
