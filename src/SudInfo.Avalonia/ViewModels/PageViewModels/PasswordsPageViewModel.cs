namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class PasswordsPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly PasswordService _passwordService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<PasswordEntity>? Passwords { get; set; }

    private IReadOnlyCollection<PasswordEntity>? PasswordsFromDatabase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public PasswordEntity? SelectedPassword { get; set; }

    #endregion

    #region Initialization
    public PasswordsPageViewModel(
        NavigationService navigationService,
        PasswordService passwordService,
        DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _passwordService = passwordService;
        _navigationService = navigationService;
        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadPasswords();
    }
    #endregion

    #region Public Methods

    public async Task LoadPasswords()
    {
        Passwords = await PasswordService.GetPasswords();
        PasswordsFromDatabase = Passwords;
    }

    public void SearchBoxKeyUp()
    {
        if (PasswordsFromDatabase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Passwords = PasswordsFromDatabase;
            return;
        }
        Passwords = PasswordsFromDatabase.Where(x => x.Description!.ToLower().Contains(SearchText.ToLower()))
                                         .ToList();
    }

    public async Task OpenAddPasswordWindow()
    {
        await _navigationService.ShowPasswordWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditPasswordWindow()
    {
        if (SelectedPassword == null)
            return;
        await _navigationService.ShowPasswordWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedPassword.Id);
    }

    public async Task RemovePassword()
    {
        if (SelectedPassword == null)
            return;
        var result = await PasswordService.RemovePassword(SelectedPassword.Id);
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления. Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        await LoadPasswords();
    }

    #endregion
}