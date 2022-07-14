using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.MVVM.Converters;

namespace Techni_Net2022_MVVM_Exo01.Converters
{
    internal class BoolToEngineBtnConverter : ConverterBase<bool, string>
    {
        public override string Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? "Stop" : "Start";
        }

        public override bool ConvertBack(string value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
