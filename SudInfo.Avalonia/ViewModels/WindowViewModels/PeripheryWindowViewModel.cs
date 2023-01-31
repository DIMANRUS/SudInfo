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
    public IEnumerable<User> Users { get; set; }
    #endregion

    #region Properties
    [Reactive]
    public Periphery Periphery { get; set; } = new();
    public bool IsUser { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить периферию";
    #endregion

    #region Constructors
    public PeripheryWindowViewModel(IPeripheryService peripheryService, IUserService usersService, IDialogService dialogService)
    {
        #region Service Set
        _peripheryService = peripheryService;
        _dialogService = dialogService;
        #endregion

        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersResult = await usersService.GetUsers();
            if (!usersResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки Сотрудников! Периферию можно добавить, но без сотрудника.", icon: Icon.Error);
                return;
            }
            Users = usersResult.Object;
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
        if (!IsUser)
            Periphery.User = null;
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
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить периферию";
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