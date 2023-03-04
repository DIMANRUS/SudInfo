namespace SudInfo.Avalonia.ViewModels.BaseViewModels;
public abstract class BaseViewModel : ReactiveObject
{
    #region Commands
    public ICommand Initialized { get; init; }
    #endregion

    public static bool ValidationModel<T>(T model) =>
        Validator.TryValidateObject(model, new(model), new List<ValidationResult>(), true);
}