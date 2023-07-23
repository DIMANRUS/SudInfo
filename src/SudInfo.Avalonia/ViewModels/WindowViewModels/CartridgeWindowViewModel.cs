namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class CartridgeWindowViewModel : BaseViewModel
{
    #region Services

    private readonly DialogService _dialogService;
    private readonly CartridgeService _cartridgeService;

    #endregion

    #region Properties

    [Reactive]
    public Cartridge Cartridge { get; set; } = new();

    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить картридж";

    [Reactive]
    public bool ButtonIsVisible { get; private set; }

    #endregion

    #region Private Fields

    private WindowType _windowType;

    #endregion

    #region Ctors

    public CartridgeWindowViewModel() { }

    public CartridgeWindowViewModel(
        DialogService dialogService,
        CartridgeService cartridgeService)
    {
        _dialogService = dialogService;
        _cartridgeService = cartridgeService;
    }

    #endregion

    #region Collections

    public static IEnumerable<CartridgeType> CartridgeTypes
        => Enum.GetValues(typeof(CartridgeType)).Cast<CartridgeType>();

    public static IEnumerable<CartridgeStatus> CartridgeStatuses
        => Enum.GetValues(typeof(CartridgeStatus)).Cast<CartridgeStatus>();

    #endregion

    #region Public methods

    public async Task SaveCartridge()
    {
        if (!ValidationModel(Cartridge))
            return;
        Result result = _windowType switch
        {
            WindowType.Add => await _cartridgeService.Add(Cartridge),
            _ => await _cartridgeService.Update(Cartridge)
        };
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", result.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }

    public async Task InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (windowType != WindowType.View)
        {
            ButtonIsVisible = true;
        }

        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                ButtonIsVisible = true;
                SaveButtonText = "Сохранить картридж";
            }

            var result = await CartridgeService.GetCartridge(id.GetValueOrDefault());
            if (!result.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения картриджа! Ошибка: {result.Message}",
                    true, icon: Icon.Error);
                return;
            }
            Cartridge = result.Object;
        }
    }

    #endregion
}