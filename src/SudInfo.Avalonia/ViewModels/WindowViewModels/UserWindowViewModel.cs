﻿namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class UserWindowViewModel : BaseViewModel
{
    #region Services
    private readonly UserService _usersService;

    #endregion

    #region Properties
    [Reactive]
    public User User { get; set; } = new();
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить пользователя";
    #endregion

    #region Private Fields

    private WindowType _windowType;

    private Action _closedWindow;

    #endregion

    #region Ctors

    public UserWindowViewModel(UserService usersService)
    {
        #region Service Set
        _usersService = usersService;

        #endregion
    }

    #endregion

    #region Public Methods

    public async Task SaveUser()
    {
        if (!ValidationModel(User))
            return;
        Result userResult = _windowType switch
        {
            WindowType.Add => await _usersService.Add(User),
            _ => await _usersService.Update(User)
        };
        if (!userResult.Success)
        {
            await DialogService.ShowErrorMessageBox(userResult.Message);
            return;
        }
        _closedWindow();
    }

    public async void Initialization(WindowType windowType, Action close, int? id = null)
    {
        _windowType = windowType;
        _closedWindow = close;

        if (id != null)
        {
            SaveButtonText = "Сохранить пользователя";
            var userResult = await _usersService.Get(id.GetValueOrDefault());
            if (!userResult.Success)
            {
                await DialogService.ShowErrorMessageBox(userResult.Message);
                return;
            }
            User = userResult.Object;
        }
    }

    #endregion
}