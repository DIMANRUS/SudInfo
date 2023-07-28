namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class PasswordWindowViewModel : BaseViewModel
{
    #region Services

    private readonly PasswordService _passwordService;

    #endregion

    #region Properties
    [Reactive]
    public PasswordEntity Password { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить пароль";
    [Reactive]
    public bool ButtonIsVisible { get; private set; } = false;
    #endregion

    #region Constructors

    public PasswordWindowViewModel(PasswordService passwordService)
    {
        #region Service Set

        _passwordService = passwordService;

        #endregion
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
            WindowType.Add => await _passwordService.Add(Password),
            _ => await _passwordService.Update(Password)
        };
        if (!result.Success)
        {
            await DialogService.ShowErrorMessageBox(result.Message);
            return;
        }
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
                SaveButtonText = "Сохранить пароль";
            }
            var result = await _passwordService.Get(id.GetValueOrDefault());
            if (!result.Success)
            {
                await DialogService.ShowErrorMessageBox(result.Message);
                return;
            }
            Password = result.Object;
        }
    }
    #endregion
}