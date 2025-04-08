using System.Globalization;
using Avalonia.Data.Converters;

namespace DWK.Controls;

public static class DebugConverters
{
    public static TypeConverter Type { get; } = new();

    public class TypeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value?.GetType().Name ?? "Null";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}