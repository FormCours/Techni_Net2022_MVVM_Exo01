using System;
using System.Globalization;
using System.Windows.Data;

namespace Tools.MVVM.Converters
{
    public abstract class ConverterBase<TIn, TOut> : IValueConverter
    {
        #region Implementation explicit de l'interface "IValueConverter"
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((TIn)value, targetType, parameter, culture);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBack((TOut)value, targetType, parameter, culture);
        }
        #endregion

        #region Méthode abstraite générique de convertion
        public abstract TOut Convert(TIn value, Type targetType, object parameter, CultureInfo culture);
        public abstract TIn ConvertBack(TOut value, Type targetType, object parameter, CultureInfo culture);
        #endregion
    }
}
