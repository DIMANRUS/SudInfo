namespace SudInfo.Avalonia.Views.Windows;
public partial class RutokenWindow : ReactiveWindow<RutokenWindowViewModel>
{
    #region Constructors
    public RutokenWindow() { }
    public RutokenWindow(WindowType windowType, int? rutokenId = null)
    {
        var rutokenWindowViewModel = ServiceCollectionExtension.ServiceProvider.GetService<RutokenWindowViewModel>();
        DataContext = rutokenWindowViewModel;
        InitializeComponent();
        rutokenWindowViewModel.InitializationData(windowType, rutokenId);
    }
    #endregion
}