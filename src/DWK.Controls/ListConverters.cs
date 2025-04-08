using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;

namespace DWK.Controls;

public static class ListConverters
{
    public static IsEmptyConverter IsEmpty { get; } = new();
    public static IsNotEmptyConverter IsNotEmpty { get; } = new();

    public class IsEmptyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is IList { Count: 0 };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNotEmptyConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is IList { Count: > 0 };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}