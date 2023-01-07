namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ComputersPageViewModel : BaseRoutableViewModel
{
    #region Services
    private IComputersService _computersService;
    private IDialogService _dialogService;
    #endregion

    #region Commands
    public ICommand OpenAddComputerWindow { get; private init; }
    public ICommand OpenEditComputerWindow { get; private init; }
    public ICommand RefreshComputers { get; private init; }
    public ICommand RemoveComputer { get; private init; }
    #endregion

    #region Collections
    [Reactive]
    public ObservableCollection<Computer> Computers { get; set; }
    #endregion

    #region Private Variables
    private EventHandler _eventHandlerClosedWindowDialog;
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
    }
    #endregion
}