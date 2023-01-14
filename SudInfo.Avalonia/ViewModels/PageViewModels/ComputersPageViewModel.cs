namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ComputersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private readonly IComputersService _computersService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Commands
    public ICommand OpenAddComputerWindow { get; private init; }
    public ICommand OpenEditComputerWindow { get; private init; }
    public ICommand RefreshComputers { get; private init; }
    public ICommand RemoveComputer { get; private init; }
    public ICommand SearchBoxKeyUp { get; private init; }
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
    public ComputersPageViewModel(INavigationService navigationService, IComputersService computersService, IDialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _computersService = computersService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadComputers();
        };

        #region Commands Initialization
        OpenAddComputerWindow = ReactiveCommand.Create(async () =>
        {
            await navigationService.ShowComputerWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
        });
        OpenEditComputerWindow = ReactiveCommand.Create(async (int id) =>
        {
            await navigationService.ShowComputerWindowDialog(WindowType.Save, _eventHandlerClosedWindowDialog, id);
        });
        Initialized = ReactiveCommand.CreateFromTask(LoadComputers);
        RefreshComputers = ReactiveCommand.CreateFromTask(LoadComputers);
        RemoveComputer = ReactiveCommand.CreateFromTask(async (int id) =>
        {
            var dialogResult = await dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить компьютер?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
            if (dialogResult == ButtonResult.No)
                return;
            var removeComputerResult = await computersService.RemoveComputerById(id);
            if (!removeComputerResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления компьютера! Ошибка: {removeComputerResult.Message}", icon: Icon.Error);
                return;
            }
            await LoadComputers();
        });
        SearchBoxKeyUp = ReactiveCommand.Create(() =>
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                Computers = new(ComputersFromDataBase);
                return;
            }
            Computers = new(ComputersFromDataBase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
        });
        #endregion
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
        Computers = new(computersResult.Result);
        ComputersFromDataBase = Computers;
    }
    #endregion
}