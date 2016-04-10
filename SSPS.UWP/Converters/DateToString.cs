using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SSPS.UWP.Converters
{
    class DateToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is DateTime))
                return null;
            if (parameter == null)
                return string.Format("{0:dd/MM/yyy}", value);
            return string.Format("{0:"+(string)parameter+"}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
