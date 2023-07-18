﻿namespace SudInfo.Avalonia.Services;

public class NavigationService
{
    #region Private Variables

    private Window? _mainWindow;
    private readonly DialogService _dialogService;

    #endregion

    #region Initialization

    public NavigationService(DialogService dialogService)
    {
        _dialogService = dialogService;
    }

    #endregion

    #region Public Methods

    public async Task ShowComputerWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? computerId = null)
    {
        if (_mainWindow == null)
            return;
        ComputerWindow computerWindow = new(windowType, computerId);
        if (closedEvent != null)
            computerWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(computerWindow);
        await computerWindow.ShowDialog(_mainWindow);
    }

    public async Task ShowMonitorWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? monitorId = null)
    {
        if (_mainWindow == null)
            return;
        MonitorWindow monitorWindow = new(windowType, monitorId);
        if (closedEvent != null)
            monitorWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(monitorWindow);
        await monitorWindow.ShowDialog(_mainWindow);
    }

    public async Task ShowPrinterWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? printerId = null)
    {
        if (_mainWindow == null)
            return;
        PrinterWindow printerWindow = new(windowType, printerId);
        if (closedEvent != null)
            printerWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(printerWindow);
        await printerWindow.ShowDialog(_mainWindow);
    }

    public async Task ShowUserWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? userId = null)
    {
        if (_mainWindow == null)
            return;
        UserWindow userWindow = new(windowType, userId);
        if (closedEvent != null)
            userWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(userWindow);
        await userWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowRutokenWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? rutokenId = null)
    {
        if (_mainWindow == null)
            return;
        RutokenWindow rutokenWindow = new(windowType, rutokenId);
        if (closedEvent != null)
            rutokenWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(rutokenWindow);
        await rutokenWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowPeripheryWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? peripheryId = null)
    {
        if (_mainWindow == null)
            return;
        PeripheryWindow peripheryWindow = new(windowType, peripheryId);
        if (closedEvent != null)
            peripheryWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(peripheryWindow);
        await peripheryWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowServerWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? id = null, ServerRack? serverRack = null)
    {
        if (_mainWindow == null)
            return;
        ServerWindow serverWindow = new(windowType, id, serverRack);
        if (closedEvent != null)
            serverWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(serverWindow);
        await serverWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowServerRackWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? id = null)
    {
        if (_mainWindow == null)
            return;
        ServerRackWindow serverRackWindow = new(windowType, id);
        if (closedEvent != null)
            serverRackWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(serverRackWindow);
        await serverRackWindow.ShowDialog(_mainWindow);
    }
    public async Task ShowTaskWindowDialog(EventHandler? closedEvent = null)
    {
        if (_mainWindow == null)
            return;
        TaskWindow taskWindow = new();
        if (closedEvent != null)
            taskWindow.Closed += closedEvent;
        _dialogService.SetCurrentWindow(taskWindow);
        await taskWindow.ShowDialog(_mainWindow);
    }

    public async Task ShowPasswordWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? id = null)
    {
        if (_mainWindow == null)
            return;
        PasswordWindow window = new(windowType, id);
        window.Closed += closedEvent;
        _dialogService.SetCurrentWindow(window);
        await window.ShowDialog(_mainWindow);
    }

    public async Task ShowAppWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? id = null)
    {
        if (_mainWindow == null)
            return;
        AppWindow window = new(windowType, id);
        window.Closed += closedEvent;
        _dialogService.SetCurrentWindow(window);
        await window.ShowDialog(_mainWindow);
    }

    public async Task ShowContactWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? id = null)
    {
        if (_mainWindow == null)
            return;
        ContactWindow window = new(windowType, id);
        window.Closed += closedEvent;
        _dialogService.SetCurrentWindow(window);
        await window.ShowDialog(_mainWindow);
    }

    public void SetWindow(Window window)
    {
        if (_mainWindow != null)
            return;
        _mainWindow = window;
    }

    public async Task ShowCartridgeWindowDialog(WindowType windowType, EventHandler? closedEvent = null, int? id = null)
    {
        if (_mainWindow == null)
            return;
        CartridgeWindow window = new(windowType, id);
        window.Closed += closedEvent;
        _dialogService.SetCurrentWindow(window);
        await window.ShowDialog(_mainWindow);
    }

    #endregion
}
public enum WindowType
{
    Add, Edit, View
}