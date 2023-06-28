namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class ContactsPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly ContactService _contactService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Private Variables

    private EventHandler _eventHandlerClosedWindowDialog;

    #endregion

    #region Properties

    [Reactive]
    public ObservableCollection<Contact>? Contacts { get; set; }

    [Reactive]
    public Contact? SelectedContact { get; set; }

    #endregion

    #region Initialization

    public ContactsPageViewModel(
        ContactService contactService,
        DialogService dialogService,
        NavigationService navigationService)
    {
        #region Services Initialization
        _contactService = contactService;
        _dialogService = dialogService;
        _navigationService = navigationService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) => await LoadContacts();
    }

    #endregion

    #region Public Methods

    public async Task LoadContacts()
    {
        var loadContactsResult = await ContactService.GetContacts();
        Contacts = new(loadContactsResult);
    }
    public async Task RemoveContact()
    {
        if (SelectedContact == null)
            return;
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить контакт?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeContactResult = await ContactService.RemoveContact(SelectedContact.Id);
        if (!removeContactResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления контакта! Ошибка: {removeContactResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadContacts();
    }
    public async Task OpenEditContactWindow()
    {
        if (SelectedContact == null)
            return;
        await _navigationService.ShowContactWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, SelectedContact.Id);
    }
    public async Task OpenAddContactWindow()
    {
        await _navigationService.ShowContactWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
    }

    #endregion
}