﻿namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class RutokenWindowViewModel : BaseViewModel
{
    #region Services

    private readonly RutokenService _rutokenService;

    private readonly UserService _usersService;

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<User>? Users { get; set; }

    #endregion

    #region Properties
    [Reactive]
    public Rutoken Rutoken { get; set; } = new();
    public bool IsUser { get; set; }
    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить рутокен";
    #endregion

    #region Constructors

    public RutokenWindowViewModel(
        RutokenService rutokenSerrvice,
        UserService usersService)
    {
        #region Service Initialization
        _rutokenService = rutokenSerrvice;

        _usersService = usersService;
        #endregion
    }

    #endregion

    #region Private Fields
    private WindowType _windowType;
    #endregion

    #region Public Methods

    public async Task SaveRutoken()
    {
        if (!IsUser)
            Rutoken.User = null;
        Result rutokenResult = _windowType switch
        {
            WindowType.Add => await _rutokenService.Add(Rutoken),
            _ => await _rutokenService.Update(Rutoken)
        };
        if (!rutokenResult.Success)
        {
            await DialogService.ShowErrorMessageBox(rutokenResult.Message);
            return;
        }
    }
    public async void InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (id != null)
        {
            SaveButtonText = "Сохранить рутокен";
            var rutokenResult = await _rutokenService.Get(id.GetValueOrDefault());
            if (!rutokenResult.Success)
            {
                await DialogService.ShowErrorMessageBox(rutokenResult.Message);
                return;
            }
            Rutoken = rutokenResult.Object;
        }
    }
    public async Task LoadUsers()
    {
        var usersResult = await _usersService.Get();
        Users = usersResult;
    }

    #endregion
}