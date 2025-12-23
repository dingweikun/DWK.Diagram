using System.Globalization;
using Avalonia.Data.Converters;

namespace DWK.Controls;

public static class SlotPositionConverters
{
    public static IsConverter Is { get; } = new IsConverter();
    public static NotConverter Not { get; } = new NotConverter();

    public class IsConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is SlotPosition pos1 && parameter is SlotPosition pos2 && pos1 == pos2;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NotConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is SlotPosition pos1 && parameter is SlotPosition pos2 && pos1 != pos2;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}