﻿namespace SudInfo.Avalonia.ViewModels.BaseViewModels;
public abstract class BaseViewModel : ReactiveObject
{
    #region Commands
    public ICommand Initialized { get; init; }
    #endregion
}