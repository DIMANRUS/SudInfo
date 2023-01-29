namespace SudInfo.Avalonia.Converters;
public class PrinterTypeToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (PrinterType)value switch
        {
            PrinterType.Принтер => "Принтер",
            PrinterType.МФУ => "МФУ",
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}