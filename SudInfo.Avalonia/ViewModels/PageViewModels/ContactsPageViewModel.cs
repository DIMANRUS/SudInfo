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

    [Reactive]
    public ObservableCollection<Contact> Contacts { get; set; }

    public ContactsPageViewModel(ContactService contactService, DialogService dialogService, NavigationService navigationService)
    {
        _contactService = contactService;
        _dialogService = dialogService;
        _navigationService = navigationService;

        _eventHandlerClosedWindowDialog = async (s, e) =>
        {
            await LoadContacts();
        };
    }

    public async Task LoadContacts()
    {
        var loadContactsResult = await _contactService.GetContacts();
        if (!loadContactsResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! {loadContactsResult.Message}", icon: Icon.Error);
            return;
        }
        Contacts = new(loadContactsResult.Object);
    }

    public async Task RemoveContact(int id)
    {
        var dialogResult = await _dialogService.ShowMessageBox("Сообщение", "Вы действительно хотите удалить контакт?", buttonEnum: ButtonEnum.YesNo, icon: Icon.Question);
        if (dialogResult == ButtonResult.No)
            return;
        var removeContactResult = await _contactService.RemoveContact(id);
        if (!removeContactResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка удаления контакта! Ошибка: {removeContactResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadContacts();
    }

    public async Task OpenEditContactWindow(int id)
    {
        await _navigationService.ShowContactWindowDialog(WindowType.Edit, _eventHandlerClosedWindowDialog, id);
    }

    public async Task OpenAddContactWindow()
    {
        await _navigationService.ShowContactWindowDialog(WindowType.Add, _eventHandlerClosedWindowDialog);
    }
}