namespace SudInfo.Avalonia.Converters;
public class PrinterTypeToTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (PrinterType)value switch
        {
            PrinterType.Printer => "Принтер",
            PrinterType.MFY => "МФУ",
            _ => string.Empty
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}