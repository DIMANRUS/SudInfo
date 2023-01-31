namespace SudInfo.Avalonia.Views.Windows;
public partial class PeripheryWindow : ReactiveWindow<PeripheryWindowViewModel>
{
    #region Constructors
    public PeripheryWindow()
    {
        InitializeComponent();
    }
    public PeripheryWindow(WindowType windowType, int? peripheryId = null)
    {
        var peripheryWindowViewModel = ServiceCollectionExtension.ServiceProvider.GetService<PeripheryWindowViewModel>();
        DataContext = peripheryWindowViewModel;
        InitializeComponent();
        peripheryWindowViewModel.InitializationData(windowType, peripheryId);
    } 
    #endregion
}