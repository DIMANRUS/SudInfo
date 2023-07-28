﻿namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class UsersPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly NavigationService _navigationService;

    private readonly UserService _userService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<User>? Users { get; set; }

    private IReadOnlyCollection<User>? UsersFromDataBase { get; set; }

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public User? SelectedUser { get; set; }

    #endregion

    #region Ctors

    public UsersPageViewModel(NavigationService navigationService, UserService userService)
    {
        #region Serives Initialization

        _navigationService = navigationService;
        _userService = userService;

        #endregion

        eventHandlerClosedWindowDialog = async (s, e) => await LoadUsers();
    }

    #endregion

    #region Public Methods

    public async Task OpenAddUserWindow()
    {
        await _navigationService.ShowUserWindowDialog(WindowType.Add, eventHandlerClosedWindowDialog);
    }

    public async Task OpenEditUserWindow()
    {
        if (SelectedUser == null)
            return;
        await _navigationService.ShowUserWindowDialog(WindowType.Edit, eventHandlerClosedWindowDialog, SelectedUser.Id);
    }

    public async Task RemoveUser()
    {
        if (SelectedUser == null)
            return;
        var dialogResult = await DialogService.ShowQuestionMessageBox("Вы действительно хотите удалить пользователя?");
        if (dialogResult == ButtonResult.No)
            return;
        var removeUserResult = await _userService.Remove(SelectedUser.Id);
        if (!removeUserResult.Success)
        {
            await DialogService.ShowErrorMessageBox(removeUserResult.Message);
            return;
        }
        await LoadUsers();
    }

    public void SearchBoxKeyUp()
    {
        if (UsersFromDataBase == null)
            return;
        if (string.IsNullOrEmpty(SearchText))
        {
            Users = UsersFromDataBase;
            return;
        }
        Users = UsersFromDataBase.Where(x => x.FIO!.ToLower().Contains(SearchText.ToLower())).ToList();
    }

    public async Task LoadUsers()
    {
        Users = await _userService.Get();
        UsersFromDataBase = Users;
    }

    #endregion
}