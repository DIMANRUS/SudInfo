namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class CartridgesPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly CartridgeService _cartridgeService;
    private readonly DialogService _dialogService;

    #endregion

    #region Properties

    [Reactive]
    public ObservableCollection<Cartridge>? Cartridges { get; set; }

    public Cartridge? SelectedCartridge { get; set; }

    [Reactive]
    public Cartridge NewCartridge { get; set; } = new();

    #endregion

    #region Initialization

    public CartridgesPageViewModel(
        CartridgeService cartridgeService,
        DialogService dialogService)
    {
        #region Services Initialization
        _cartridgeService = cartridgeService;
        _dialogService = dialogService;
        #endregion
    }

    #endregion

    #region Public methods

    public async Task AddCartridge()
    {
        if (!ValidationModel(NewCartridge))
            return;
        var result = await _cartridgeService.Add(NewCartridge);
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка добавления картриджа! Возможно, что такой картридж уже есть. Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        await LoadCartridges();
        NewCartridge = new();
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