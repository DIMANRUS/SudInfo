namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class PasswordWindowViewModel : BaseViewModel
{
    #region Services
    private readonly PasswordService _passwordService;
    private readonly DialogService _dialogService;
    #endregion

    #region Properties
    [Reactive]
    public PasswordEntity Password { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить компьютер";
    [Reactive]
    public bool ButtonIsVisible { get; private set; } = false;
    #endregion

    #region Constructors
    public PasswordWindowViewModel(PasswordService passwordService, DialogService dialogService)
    {
        #region Service Set
        _passwordService = passwordService;
        _dialogService = dialogService;
        #endregion
    }

    public PasswordWindowViewModel()
    {
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Public Methods
    public async Task SavePassword()
    {
        if (!ValidationModel(Password))
            return;
        Result result = _windowType switch
        {
            WindowType.Add => await _passwordService.AddPassword(Password),
            _ => await _passwordService.UpdatePassword(Password)
        };
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", result.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
    public async void InitializationData(WindowType windowType, int? id = null)
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
                SaveButtonText = "Сохранить компьютер";
            }
            var result = await _passwordService.GetPasswordEntity(id.GetValueOrDefault());
            if (!result.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения компьютера! Ошибка: {result.Message}", true, icon: Icon.Error);
                return;
            }
            Password = result.Object;
        }
    }
    #endregion
}