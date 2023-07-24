namespace SudInfo.Avalonia.ViewModels.BaseViewModels;

public abstract class BaseViewModel : ReactiveObject
{
    [Reactive]
    public bool IsLoading { get; set; } = true;

    public static bool ValidationModel<T>(T model)
    {
        if (model == null)
            return false;
        return Validator.TryValidateObject(model, new(model), new List<ValidationResult>(), true);
    }
}