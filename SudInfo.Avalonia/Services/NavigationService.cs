namespace SudInfo.Avalonia.Services;
public class NavigationService : INavigationService
{
    #region Private Variables
    private Window _mainWindow;
    private readonly IDialogService _dialogService;
    #endregion

    #region Constructors
    public NavigationService(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }
    #endregion

    #region Public Methods
    public async Task ShowComputerWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? computerId = null)
    {
        ComputerWindow computerWindow = new(windowType, computerId);
        if (closedEvent != null)
            computerWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(computerWindow);
        await computerWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowMonitorWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? monitorId = null)
    {
        MonitorWindow monitorWindow = new(windowType, monitorId);
        if (closedEvent != null)
            monitorWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(monitorWindow);
        await monitorWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowPrinterWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? printerId = null)
    {
        PrinterWindow printerWindow = new(windowType, printerId);
        if (closedEvent != null)
            printerWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(printerWindow);
        await printerWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowUserWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? userId = null)
    {
        UserWindow userWindow = new(windowType, userId);
        if (closedEvent != null)
            userWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(userWindow);
        await userWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowSettingsWindowDialog()
    {
        await new SettingsWindow().ShowDialog(_mainWindow);
    }
    public void SetWindow(Window window)
    {
        if (_mainWindow != null)
            return;
        _mainWindow = window;
    }
    #endregion
}
public enum WindowType
{
    Save, Add
}