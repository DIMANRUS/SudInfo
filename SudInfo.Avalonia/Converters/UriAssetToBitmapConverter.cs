//namespace SudInfo.Avalonia.Converters;
//internal class UriAssetToBitmapConverter : IValueConverter
//{
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
//        return new Bitmap(assets.Open(new(value.ToString())));
//    }

//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}