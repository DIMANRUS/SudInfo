namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ComputersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly ComputerService _computersService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Computer> Computers { get; set; }
    private IEnumerable<Computer> ComputersFromDataBase { get; set; }
    #endregion

    #region Private Variables
    private EventHandler _eventHandlerClosedWindowDialog;
    #endregion

    #region Public properties
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public ComputersPageViewModel(NavigationService navigationService, ComputerService computersService, DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _computersService = computersService;
        _navigationService = navigationService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadComputers();
        };

        Initialized = ReactiveCommand.CreateFromTask(LoadComputers);
    }
    #endregion

    #region Public Methods
    public async Task OpenAddComputerWindow()
    {
        await _navigationService.ShowComputerWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditComputerWindow(int id)
    {
        await _navigationService.ShowComputerWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, id);
    }
    public async Task RefreshComputers()
    {
        await LoadComputers();
    }
    public async Task RemoveComputer(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить компьютер?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeComputerResult = await _computersService.RemoveComputerById(id);
        if (!removeComputerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления компьютера! Ошибка: {removeComputerResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadComputers();
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Computers = new(ComputersFromDataBase);
            return;
        }
        Computers = new(ComputersFromDataBase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
    }
    #endregion

    #region Private Methods
    private async Task LoadComputers()
    {
        var computersResult = await _computersService.GetComputers();
        if (!computersResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {computersResult.Message}", icon: Icon.Error);
            return;
        }
        Computers = new(computersResult.Object);
        ComputersFromDataBase = Computers;
    }
    #endregion
}