namespace SudInfo.WPF.ViewModels;
public abstract class BaseViewModel : INotifyPropertyChanged
{
    #region Private fields
    private bool _isLoading = true;
    #endregion

    #region Properties
    public bool IsLoading
    {
        get => _isLoading;
        set => Set(value, ref _isLoading);
    }
    #region Commands
    public ICommand Loaded { get; set; }
    #endregion
    #endregion

    #region INotifyPropertyChanged Realization
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnNotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void Set<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
    {
        if (value == null || value.Equals(field))
            return;
        field = value;
        OnNotifyPropertyChanged(propertyName);
    }
    #endregion
}