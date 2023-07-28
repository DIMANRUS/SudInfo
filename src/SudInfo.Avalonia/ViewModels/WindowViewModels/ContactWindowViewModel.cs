namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class ContactWindowViewModel : BaseViewModel
{
    #region Services

    private readonly ContactService _contactService;

    #endregion

    #region Properties
    [Reactive]
    public Contact Contact { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить контакт";
    [Reactive]
    public bool ButtonIsVisible { get; private set; } = false;
    #endregion

    #region Constructors
    public ContactWindowViewModel(ContactService contactService)
    {
        #region Service Set

        _contactService = contactService;

        #endregion
    }
    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Public Methods

    public async Task SaveContact()
    {
        if (!ValidationModel(Contact))
            return;
        Result result = _windowType switch
        {
            WindowType.Add => await _contactService.Add(Contact),
            _ => await _contactService.Update(Contact)
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
                SaveButtonText = "Сохранить контакт";
            }
            var result = await _contactService.Get(id.GetValueOrDefault());
            if (!result.Success)
            {
                await DialogService.ShowErrorMessageBox(result.Message);
                return;
            }
            Contact = result.Object;
        }
    }

    #endregion
}