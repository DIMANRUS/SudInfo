namespace SudInfo.Avalonia.Interfaces;
public interface INavigationService
{
    Task ShowComputerWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? computerId = null);
    Task ShowPrinterWindowDialog(WindowType windowType, EventHandler closedEvent = null, int? printerId = null);
    void SetWindow(Window window);
    Task ShowSettingsWindowDialog();
}