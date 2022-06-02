using System;
using System.Globalization;
using System.Windows.Data;

namespace Wallet.View
{
    public class OutgoTransactionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            decimal sum = 0;
            try
            {
                sum = Decimal.Parse(values[0] as string);
            }
            catch { }

            return $"{sum}||{values[1]}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
