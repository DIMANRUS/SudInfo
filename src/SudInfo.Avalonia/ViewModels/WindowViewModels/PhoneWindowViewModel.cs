namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class PhoneWindowViewModel : BaseViewModel
{
    #region Services

    private readonly PhoneService _phoneService;

    private readonly UserService _userService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<User>? Users { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public Phone Phone { get; set; } = new();

    [Reactive]
    public bool IsUser { get; set; }

    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить телефон";

    [Reactive]
    public bool ButtonIsVisible { get; private set; }

    #endregion

    #region Ctors

    public PhoneWindowViewModel(PhoneService phoneService, UserService userService)
    {
        #region Service Initialization

        _phoneService = phoneService;
        _userService = userService;

        #endregion
    }

    #endregion

    #region Private Fields

    private WindowType _windowType;

    private Action _closedWindow;

    #endregion

    #region Public Methods

    public async Task SavePhone()
    {
        if (!ValidationModel(Phone))
            return;
        if (!IsUser)
            Phone.User = null;
        if (!Phone.IsBroken)
            Phone.BreakdownDescription = string.Empty;
        Result computerResult = _windowType switch
        {
            WindowType.Add => await _phoneService.Add(Phone),
            _ => await _phoneService.Update(Phone)
        };
        if (!computerResult.Success)
        {
            await DialogService.ShowErrorMessageBox(computerResult.Message);
            return;
        }
        _closedWindow();
    }

    public async void Initialization(WindowType windowType, Action close, int? id = null)
    {
        var usersResult = await _userService.Get();
        Users = usersResult;

        _windowType = windowType;
        _closedWindow = close;

        if (windowType != WindowType.View)
        {
            ButtonIsVisible = true;
        }
        if (id == null)
            return;
        if (windowType != WindowType.View)
        {
            ButtonIsVisible = true;
            SaveButtonText = "Сохранить телефон";
        }
        var result = await _phoneService.Get(id.GetValueOrDefault());
        if (!result.Success)
        {
            await DialogService.ShowErrorMessageBox(result.Message);
            return;
        }
        IsUser = result.Object?.User != null;
        if (result.Object?.User != null)
            result.Object.User = Users.First(x => x.Id == result.Object.User.Id);
        Phone = result.Object;
    }

    #endregion
}