namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ComputersPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly ComputerService _computersService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    private readonly ExcelService _excelService;

    #endregion

    #region Collections

    [Reactive]
    public ObservableCollection<Computer>? Computers { get; set; }

    private IReadOnlyList<Computer>? ComputersFromDataBase { get; set; }

    #endregion

    #region Private Variables

    private readonly EventHandler _eventHandlerClosedWindowDialog;

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Computer? SelectedComputer { get; set; }

    #endregion

    #region Initialization

    public ComputersPageViewModel(
        NavigationService navigationService,
        ComputerService computersService,
        DialogService dialogService,
        ExcelService excelService)
    {
        #region Services Initialization

        _dialogService = dialogService;
        _computersService = computersService;
        _navigationService = navigationService;
        _excelService = excelService;

        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) => await LoadComputers();
    }

    #endregion

    #region Public Methods

    public async Task OpenAddComputerWindow()
    {
        await _navigationService.ShowComputerWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditComputerWindow()
    {
        if (SelectedComputer == null)
            return;
        await _navigationService.ShowComputerWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, SelectedComputer.Id);
    }

    public async Task RemoveComputer()
    {
        if (SelectedComputer == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение",
            "Вы действительно хотите удалить компьютер?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeComputerResult = await ComputerService.RemoveComputerById(SelectedComputer.Id);
        if (!removeComputerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка",
                $"Ошибка удаления компьютера! Ошибка: {removeComputerResult.Message}", icon: Icon.Error);
            return;
        }

        await LoadComputers();
    }

    public async Task CreateExcelTable()
    {
        if (Computers == null || Computers.Count == 0)
            return;
        await ExcelService.CreateExcelTableFromEntity(Computers);
    }

    public void SearchBoxKeyUp()
    {
        if (ComputersFromDataBase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Computers = new(ComputersFromDataBase);
            return;
        }
        Computers = new(ComputersFromDataBase.Where(x => x.Name!.ToLower().Contains(SearchText.ToLower()) ||
                                                    x.InventarNumber!.Contains(SearchText) ||
                                                    x.SerialNumber!.Contains(SearchText) ||
                                                    x.User != null &&
                                                    x.User.FIO.ToLower().Contains(SearchText.ToLower())));
    }

    public async Task LoadComputers()
    {
        SearchText = string.Empty;
        var computersResult = await ComputerService.GetComputers();
        Computers = new(computersResult);
        ComputersFromDataBase = Computers;
    }

    #endregion
}