namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class PeripheryPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly PeripheryService _peripheryService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public ObservableCollection<Periphery>? Peripheries { get; set; }
    private IEnumerable<Periphery>? PeripheriesFromDatabase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Periphery? SelectedPeriphery { get; set; }

    #endregion

    #region Initialization

    public PeripheryPageViewModel(
        PeripheryService peripheryService,
        DialogService dialogService,
        NavigationService navigationService)
    {
        #region Services Initialization
        _peripheryService = peripheryService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadPeripheries();
    }

    #endregion

    #region Public Methods

    public async Task RefreshPeriphery()
    {
        await LoadPeripheries();
    }
    public async Task OpenAddPeripheryWindow()
    {
        await _navigationService.ShowPeripheryWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditPeripheryWindow()
    {
        if (SelectedPeriphery == null)
            return;
        await _navigationService.ShowPeripheryWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedPeriphery.Id);
    }
    public async Task RemovePeriphery()
    {
        if (SelectedPeriphery == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить периферию?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removePeripheryResult = await PeripheryService.RemovePeripheryById(SelectedPeriphery.Id);
        if (!removePeripheryResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления периферии! Ошибка: {removePeripheryResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadPeripheries();
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Peripheries = new(PeripheriesFromDatabase);
            return;
        }
        Peripheries = new(PeripheriesFromDatabase.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
    }
    public async Task LoadPeripheries()
    {
        var peripheriesResult = await PeripheryService.GetPeripheryList();
        Peripheries = new(peripheriesResult);
        PeripheriesFromDatabase = Peripheries;
    }

    #endregion
}