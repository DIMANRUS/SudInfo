﻿namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class MonitorsPageViewModel : BaseRoutableViewModel
{
    #region Services


    private readonly MonitorService _monitorService;

    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<Monitor>? Monitors { get; set; }

    private IReadOnlyCollection<Monitor>? MonitorsFromDataBase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Monitor? SelectedMonitor { get; set; }

    #endregion

    #region Ctors

    public MonitorsPageViewModel(
        NavigationService navigationService,
        MonitorService monitorsService)
    {
        #region Serives Initialization

        _monitorService = monitorsService;
        _navigationService = navigationService;

        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadMonitors();
    }

    #endregion

    #region Public Methods

    public async Task OpenAddMonitorWindow()
    {
        await _navigationService.ShowMonitorWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditMonitorWindow()
    {
        if (SelectedMonitor == null)
            return;
        await _navigationService.ShowMonitorWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedMonitor.Id);
    }

    public async Task RemoveMonitor()
    {
        if (SelectedMonitor == null)
            return;
        var dialogResult = await DialogService.ShowQuestionMessageBox("Вы действительно хотите удалить монитор?");
        if (dialogResult == ButtonResult.No)
            return;
        var removeMonitorResult = await _monitorService.Remove(SelectedMonitor.Id);
        if (!removeMonitorResult.Success)
        {
            await DialogService.ShowErrorMessageBox(removeMonitorResult.Message);
            return;
        }
        await LoadMonitors();
    }

    public void SearchBoxKeyUp()
    {
        if (MonitorsFromDataBase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Monitors = MonitorsFromDataBase;
            return;
        }
        Monitors = MonitorsFromDataBase.Where(x => x.Name!.ToLower().Contains(SearchText.ToLower()) ||
                                                    x.InventarNumber!.Contains(SearchText) ||
                                                    x.SerialNumber!.Contains(SearchText) ||
                                                    x.Computer != null &&
                                                    x.Computer.User != null &&
                                                    x.Computer.User.FIO.ToLower().Contains(SearchText.ToLower()))
                                       .ToList();
    }

    public async Task LoadMonitors()
    {
        Monitors = await _monitorService.Get();
        MonitorsFromDataBase = Monitors;
    }

    #endregion
}