﻿namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class CartridgeWindowViewModel : BaseViewModel
{
    #region Services

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
    private Action _closedWindow;

    #endregion

    #region Ctors

    public CartridgeWindowViewModel() { }

    public CartridgeWindowViewModel(

        CartridgeService cartridgeService)
    {
        _cartridgeService = cartridgeService;
    }

    #endregion

    #region Collections

    public static IEnumerable<CartridgeType> CartridgeTypes
        => Enum.GetValues<CartridgeType>().Cast<CartridgeType>();

    public static IEnumerable<CartridgeStatus> CartridgeStatuses
        => Enum.GetValues<CartridgeStatus>().Cast<CartridgeStatus>();

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
            await DialogService.ShowErrorMessageBox(result.Message);
            return;
        }
        _closedWindow();
    }

    public async Task Initialization(WindowType windowType, Action close, int? id = null)
    {
        _windowType = windowType;
        _closedWindow = close;
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

            var result = await _cartridgeService.Get(id.GetValueOrDefault());
            if (!result.Success)
            {
                await DialogService.ShowErrorMessageBox(result.Message);
                return;
            }
            Cartridge = result.Object;
        }
    }

    #endregion
}