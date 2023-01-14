namespace SudInfo.Avalonia.Views.Windows;
public partial class ComputerWindow : ReactiveWindow<ComputerWindowViewModel>
{
    #region Constructors
    public ComputerWindow() { }
    public ComputerWindow(WindowType windowType, int? computerId = null)
    {
        var computerWindowViewModel = ServiceCollectionExtension.ServiceProvider.GetService<ComputerWindowViewModel>();
        DataContext = computerWindowViewModel;
        InitializeComponent();
        computerWindowViewModel.InitializationData(windowType, computerId);
    }
    #endregion
}