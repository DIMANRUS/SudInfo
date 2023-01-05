namespace SudInfo.Avalonia.Services;
public class NavigationService : INavigationService
{
    private Window _mainWindow;

    public void SetWindow(Window window)
    {
        if (_mainWindow != null)
            return;
        _mainWindow = window;
    }

    public async Task ShowComputerWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? computerId = null)
    {
        ComputerWindow computerWindow = new(windowType, computerId);
        if (closedEvent != null)
            computerWindow.Closed += closedEvent;
        await computerWindow.ShowDialog(_mainWindow);
    }

    public async Task ShowPrinterWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? printerId = null)
    {
        PrinterWindow printerWindow = new(windowType, printerId);
        if (closedEvent != null)
            printerWindow.Closed += closedEvent;
        await printerWindow.ShowDialog(_mainWindow);
    }

    public async Task ShowSettingsWindowDialog()
    {
        await new SettingsWindow().ShowDialog(_mainWindow);
    }

    public enum WindowType
    {
        Save, Add
    }
}