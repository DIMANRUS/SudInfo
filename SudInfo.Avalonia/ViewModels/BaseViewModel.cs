namespace SudInfo.Avalonia.ViewModels;
public abstract class BaseViewModel : ReactiveObject
{
    #region Commands
    public ICommand Initialized { get; init; }
    #endregion
}