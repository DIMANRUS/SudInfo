namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class PeripheryWindowViewModel : BaseViewModel
{
    #region Services
    private readonly IPeripheryService _peripheryService;
    private readonly IDialogService _dialogService;
    #endregion

    #region Collections
    public static IEnumerable<PeripheryType> PeripheryTypes => Enum.GetValues(typeof(PeripheryType)).Cast<PeripheryType>();
    [Reactive]
    public IEnumerable<Computer> Computers { get; set; }
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

    #region Constructors
    public PeripheryWindowViewModel(IPeripheryService peripheryService, IDialogService dialogService, IComputerService computerService)
    {
        #region Service Set
        _peripheryService = peripheryService;
        _dialogService = dialogService;
        #endregion

        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var computersResult = await computerService.GetComputerNames();
            if (!computersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки компьютеров!", icon: Icon.Error);
                return;
            }
            Computers = computersResult.Object;
        });
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
        if (!IsComputer)
            Periphery.Computer = null;
        Result computerResult = _windowType switch
        {
            WindowType.Add => await _peripheryService.AddPeriphery(Periphery),
            _ => await _peripheryService.UpdatePeriphery(Periphery)
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
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                IsButtonVisible = true;
                SaveButtonText = "Сохранить периферию";
            }
            var peripheryResult = await _peripheryService.GetPeripheryById(id.GetValueOrDefault());
            if (!peripheryResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения периферии! Ошибка: {peripheryResult.Message}", true, icon: Icon.Error);
                return;
            }
            Periphery = peripheryResult.Object;
        }
    }
    #endregion
}