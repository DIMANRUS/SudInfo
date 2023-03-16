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
    public ObservableCollection<PasswordEntity> Passwords { get; set; }
    private IReadOnlyList<PasswordEntity> PasswordsFromDatabase { get; set; }
    #endregion

    #region Private Variables
    private readonly EventHandler _eventHandlerClosedWindowDialog;
    #endregion

    #region Public properties
    [Reactive]
    public string SearchText { get; set; } = string.Empty;
    #endregion

    #region Constructors
    public PasswordsPageViewModel(NavigationService navigationService, PasswordService passwordService, DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _passwordService = passwordService;
        _navigationService = navigationService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) =>
            await LoadPasswords();
    }
    #endregion

    #region Public Methods
    public async Task LoadPasswords()
    {
        var result = await _passwordService.GetPasswords();
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        Passwords = new(result.Object);
        PasswordsFromDatabase = Passwords;
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Passwords = new(PasswordsFromDatabase);
            return;
        }
        Passwords = new(PasswordsFromDatabase.Where(x => x.Description.ToLower().Contains(SearchText.ToLower())));
    }
    public async Task OpenAddPasswordWindow()
    {
        await _navigationService.ShowPasswordWindowDialog(WindowType.Add,_eventHandlerClosedWindowDialog);
    }
    public async Task OpenEditPasswordWindow(int id)
    {
        await _navigationService.ShowPasswordWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, id);
    }
    public async Task RemovePassword(int id)
    {
        var result = await _passwordService.RemovePassword(id);
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления. Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        await LoadPasswords();
    }
    #endregion
}