namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class PasswordsPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly NavigationService _navigationService;

    private readonly PasswordService _passwordService;

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

    #region Ctors

    public PasswordsPageViewModel(NavigationService navigationService, PasswordService passwordService)
    {
        #region Serives Initialization

        _navigationService = navigationService;
        _passwordService = passwordService;

        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadPasswords();

    }

    #endregion

    #region Public Methods

    public async Task LoadPasswords()
    {
        Passwords = await _passwordService.Get();
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
        var result = await _passwordService.Remove(SelectedPassword.Id);
        if (!result.Success)
        {
            await DialogService.ShowErrorMessageBox(result.Message);
            return;
        }
        await LoadPasswords();
    }

    #endregion
}