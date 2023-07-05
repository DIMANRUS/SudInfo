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

    public static IEnumerable<PeripheryType> PeripheryTypes => Enum.GetValues(typeof(PeripheryType)).Cast<PeripheryType>();

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public Periphery? SelectedPeriphery { get; set; }

    [Reactive]
    public PeripheryType SelectedPeripheryType { get; set; } = Enum.GetValues(typeof(PeripheryType)).Cast<PeripheryType>().First();

    [Reactive]
    public bool IsPeripheryTypeFilter { get; set; }

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

        eventHandlerClosedWindowDialog = async (s, e) => await RefreshPeriphery();
    }

    #endregion

    #region Public Methods

    public async Task RefreshPeriphery()
    {
        await LoadPeripheries();
        SearchBoxKeyUp();
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

    public void SearchBoxKeyUp(object? checkedChangedEventArgs = null)
    {
        if (checkedChangedEventArgs != null)
        {
            IsPeripheryTypeFilter = (bool)((CheckBox)((RoutedEventArgs)checkedChangedEventArgs).Source).IsChecked;
        }
        if (PeripheriesFromDatabase == null || Peripheries == null)
        {
            return;
        }
        Peripheries = new(PeripheriesFromDatabase.Where(x => x.Name!.ToLower().Contains(SearchText.ToLower()) ||
                                                             x.InventarNumber!.Contains(SearchText) ||
                                                             x.SerialNumber!.Contains(SearchText) ||
                                                             x.Computer != null &&
                                                             x.Computer.User != null &&
                                                             x.Computer.User.FIO.ToLower().Contains(SearchText.ToLower())));
        if (IsPeripheryTypeFilter)
            Peripheries = new(Peripheries.Where(x => IsPeripheryTypeFilter &&
                                                 x.Type == SelectedPeripheryType));
    }

    public void SelectionPeripheryTypeChanged(object selectionChangedEventArgs)
    {
        if (Peripheries == null)
            return;
        PeripheryType selectedType = (PeripheryType)((ComboBox)((SelectionChangedEventArgs)selectionChangedEventArgs).Source).SelectedItem;
        SelectedPeripheryType = selectedType;
        SearchBoxKeyUp();
    }

    public async Task LoadPeripheries()
    {
        var peripheriesResult = await PeripheryService.GetPeripheryList();
        Peripheries = new(peripheriesResult);
        PeripheriesFromDatabase = Peripheries;
    }

    #endregion
}