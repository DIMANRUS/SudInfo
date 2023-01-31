namespace SudInfo.Avalonia.Interfaces;
public interface INavigationService
{
    Task ShowComputerWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? computerId = null);
    Task ShowPrinterWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? printerId = null);
    Task ShowMonitorWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? monitorId = null);
    Task ShowUserWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? userId = null);
    Task ShowRutokenWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? rutokenId = null);
    Task ShowPeripheryWindowDialog (WindowType windowType, EventHandler closedEvent = null, int? peripheryId = null);
    void SetWindow(Window window);
    Task ShowSettingsWindowDialog();
}