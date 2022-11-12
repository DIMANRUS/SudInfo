using SudInfo.Avalonia.Extensions;

namespace SudInfo.Avalonia.Views.Windows;
public partial class PrinterWindow : ReactiveWindow<PrinterWindowViewModel>
{
    public PrinterWindow() { }
    public PrinterWindow(WindowType windowType, int? computerId = null)
    {
        var printerWindowViewModel = ServiceCollectionExtension.ServiceProvider.GetService<PrinterWindowViewModel>();
        DataContext = printerWindowViewModel;
        InitializeComponent();
        printerWindowViewModel.InitializationData(windowType, computerId);
    }
}