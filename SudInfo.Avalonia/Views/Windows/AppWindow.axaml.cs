using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SudInfo.Avalonia.Views.Windows;

public partial class AppWindow : ReactiveWindow<AppWindowViewModel>
{
    public AppWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    public AppWindow(WindowType windowType, int? id = null)
    {
        var viewModel = ServiceCollectionExtension.ServiceProvider.GetService<AppWindowViewModel>();
        DataContext = viewModel;
        InitializeComponent();
        viewModel.InitializationData(windowType, id);
    }
}