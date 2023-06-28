using SudInfo.Avalonia.Services;

namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class PeripheryWindowViewModel : BaseViewModel
{
    #region Services

    private readonly PeripheryService _peripheryService;
    private readonly DialogService _dialogService;
    private readonly ComputerService _computerService;

    #endregion

    #region Collections

    public static IEnumerable<PeripheryType> PeripheryTypes => Enum.GetValues(typeof(PeripheryType)).Cast<PeripheryType>();

    [Reactive]
    public IEnumerable<Computer>? Computers { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public Periphery Periphery { get; set; } = new();

    public bool IsComputer { get; set; }

    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить периферию";

    [Reactive]
    public bool IsButtonVisible { get; set; } = false;

    #endregion

    #region Initialization

    public PeripheryWindowViewModel(
        PeripheryService peripheryService,
        DialogService dialogService,
        ComputerService computerService)
    {
        #region Service Initialization
        _peripheryService = peripheryService;
        _dialogService = dialogService;
        _computerService = computerService;
        #endregion
    }

    public PeripheryWindowViewModel()
    {
    }

    #endregion

    #region Private Fields

    private WindowType _windowType;

    #endregion

    #region Public Methods

    public async Task SavePeriphery()
    {
        if (!ValidationModel(Periphery))
            return;
        if (!IsComputer)
            Periphery.Computer = null;
        Result computerResult = _windowType switch
        {
            WindowType.Add => await PeripheryService.AddPeriphery(Periphery),
            _ => await _peripheryService.Update(Periphery)
        };
        if (!computerResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", computerResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    public async Task InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (windowType != WindowType.View)
            IsButtonVisible = true;
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                SaveButtonText = "Сохранить периферию";
            }
            var peripheryResult = await PeripheryService.GetPeripheryById(id.GetValueOrDefault());
            if (!peripheryResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения периферии! Ошибка: {peripheryResult.Message}", true, icon: Icon.Error);
                return;
            }
            Periphery = peripheryResult.Object;
        }
    }
    public async Task LoadComputers()
    {
        var computersResult = await ComputerService.GetComputerNames();
        Computers = computersResult;
    }

    #endregion
}