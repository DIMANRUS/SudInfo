namespace SudInfo.Avalonia.Views.Windows;
public partial class ServerWindow : ReactiveWindow<ServerWindowViewModel>
{
    #region Constructors
    public ServerWindow() { }
    public ServerWindow(WindowType windowType, int? id = null, ServerRack serverRack = null)
    {
        var viewModel = ServiceCollectionExtension.ServiceProvider.GetService<ServerWindowViewModel>();
        DataContext = viewModel;
        InitializeComponent();
        viewModel.InitializationData(windowType, id, serverRack);
    }
    #endregion
}