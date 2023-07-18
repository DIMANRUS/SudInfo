namespace SudInfo.Avalonia.Views.Windows;

public partial class ComputerWindow : ReactiveWindow<ComputerWindowViewModel>
{
    #region Constructors
    public ComputerWindow() { }
    public ComputerWindow(WindowType windowType, int? computerId = null)
    {
        InitializeComponent();
        var computerWindowViewModel = ServiceCollectionExtension.ServiceProvider.GetService<ComputerWindowViewModel>();
        DataContext = computerWindowViewModel;
        computerWindowViewModel.InitializationData(windowType, computerId);
    }
    #endregion
}