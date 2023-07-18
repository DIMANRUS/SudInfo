namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class CartridgesPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly CartridgeService _cartridgeService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Properties

    [Reactive]
    public ObservableCollection<Cartridge>? Cartridges { get; set; }

    public Cartridge? SelectedCartridge { get; set; }

    [Reactive]
    public Cartridge NewCartridge { get; set; } = new();

    #endregion

    #region Ctors

    public CartridgesPageViewModel(
        CartridgeService cartridgeService,
        DialogService dialogService,
        NavigationService navigationService)
    {
        #region Services Initialization

        _cartridgeService = cartridgeService;
        _dialogService = dialogService;
        _navigationService = navigationService;

        #endregion

        eventHandlerClosedWindowDialog += async (s, e) =>
        {
            await LoadCartridges();
        };
    }

    #endregion

    #region Public methods

    public async Task OpenEditCartridgeWindow()
    {
        if (SelectedCartridge == null)
            return;
        await _navigationService.ShowCartridgeWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedCartridge.Id);
    }
    public async Task OpenAddCartridgeWindow()
    {
        await _navigationService.ShowCartridgeWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }
    public async Task LoadCartridges()
    {
        var loadCartridgesResult = await CartridgeService.GetCartridges();
        Cartridges = new(loadCartridgesResult);
    }
    public async Task SaveCartridges()
    {
        if (Cartridges == null || Cartridges.Count == 0)
            return;
        var saveCartridgesResult = await _cartridgeService.Update(Cartridges.ToArray());
        if (!saveCartridgesResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {saveCartridgesResult.Message}", icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Успешно", $"Сохранено!", icon: Icon.Success);
    }
    public async Task RemoveCartridge()
    {
        if (SelectedCartridge == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение",
           "Вы действительно хотите удалить картридж?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeCartridgesResult = await CartridgeService.Remove(SelectedCartridge.Id);
        if (!removeCartridgesResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления! Ошибка: {removeCartridgesResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadCartridges();
    }

    #endregion
}