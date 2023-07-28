namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class PeripheryWindowViewModel : BaseViewModel
{
    #region Services

    private readonly PeripheryService _peripheryService;

    private readonly ComputerService _computerService;

    #endregion

    #region Collections

    public static IEnumerable<PeripheryType> PeripheryTypes => Enum.GetValues(typeof(PeripheryType)).Cast<PeripheryType>();

    [Reactive]
    public IReadOnlyCollection<Computer>? Computers { get; set; }

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

        ComputerService computerService)
    {
        #region Service Initialization
        _peripheryService = peripheryService;

        _computerService = computerService;
        #endregion
    }

    public PeripheryWindowViewModel()
    {
    }

    #endregion

    #region Private Fields

    private WindowType _windowType;

    private Action _closedWindow;

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
            WindowType.Add => await _peripheryService.Add(Periphery),
            _ => await _peripheryService.Update(Periphery)
        };
        if (!computerResult.Success)
        {
            await DialogService.ShowErrorMessageBox(computerResult.Message);
            return;
        }
        _closedWindow();
    }
    public async Task Initialization(WindowType windowType, Action close, int? id = null)
    {
        _windowType = windowType;
        _closedWindow = close;

        if (windowType != WindowType.View)
            IsButtonVisible = true;
        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                SaveButtonText = "Сохранить периферию";
            }
            var peripheryResult = await _peripheryService.Get(id.GetValueOrDefault());
            if (!peripheryResult.Success)
            {
                await DialogService.ShowErrorMessageBox(peripheryResult.Message);
                return;
            }
            Periphery = peripheryResult.Object;
        }
    }
    public async Task LoadComputers()
    {
        var computersResult = await _computerService.GetComputerNamesWithUser();
        Computers = computersResult;
    }

    #endregion
}