namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class CartridgesPageViewModel : BaseRoutableViewModel
{
    private readonly CartridgeService _cartridgeService;
    private readonly DialogService _dialogService;

    [Reactive]
    public ObservableCollection<Cartridge> Cartridges { get; set; }
    public string CartridgeName { get; set; }

    public CartridgesPageViewModel(CartridgeService cartridgeService, DialogService dialogService)
    {
        _cartridgeService = cartridgeService;
        _dialogService = dialogService;
    }

    public async Task AddCartridge()
    {
        var result = await _cartridgeService.Add(CartridgeName);
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка добавления картриджа! Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        await LoadCartridges();
    }

    public async Task LoadCartridges()
    {
        var loadCartridgesResult = await _cartridgeService.GetCartridges();
        if (!loadCartridgesResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {loadCartridgesResult.Message}", icon: Icon.Error);
            return;
        }
        Cartridges = new(loadCartridgesResult.Object);
    }

    public async Task SaveCartridges()
    {
        var saveCartridgesResult = await _cartridgeService.Update(Cartridges.ToArray());
        if (!saveCartridgesResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {saveCartridgesResult.Message}", icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Успешно", $"Сохранено!", icon: Icon.Success);
    }

    public async Task RemoveCartridge(int id)
    {
        var removeCartridgesResult = await _cartridgeService.Remove(id);
        if (!removeCartridgesResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления! Ошибка: {removeCartridgesResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadCartridges();
    }
}