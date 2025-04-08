using System.Globalization;
using Avalonia.Data.Converters;

namespace DWK.Controls;

public static class SlotItemConverters
{
    public static SlotItemContentConverter Content { get; } = new();

    public class SlotItemContentConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is SlotItem item ? item.Content : null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}