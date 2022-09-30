namespace SudInfo.WPF.Windows;
public partial class MainWindow : Window
{
    #region Initialization
    public MainWindow()
    {
        InitializeComponent();
        WindowDialogStore.OpenAddComputerWindowDialog = () =>
        {
            new AddComputerWindow().ShowDialog();
        };
        ActionStore.CloseWindow = () =>
        {
            Close();
        };
    }
    #endregion
}