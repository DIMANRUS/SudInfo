namespace SudInfo.Avalonia.ViewModels.BaseViewModels;

public abstract class BaseRoutableViewModel : BaseViewModel, IRoutableViewModel
{
    #region IRoutableViewModel Realization

    public string? UrlPathSegment { get; }
    public IScreen HostScreen => Locator.Current.GetService<IScreen>()!;

    #endregion

    #region Protected Variables

    protected EventHandler? eventHandlerClosedWindowDialog;

    #endregion
}