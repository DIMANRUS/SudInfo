namespace SudInfo.WPF.ViewModels;
public class ComputersPageViewModel : BaseViewModel
{
    public ComputersPageViewModel()
    {
        Loaded = new ActionCommand(() =>
        {
            try
            {
                using ApplicationDBContext context = new();
                
                Computers = new(context.Computers);
                OnNotifyPropertyChanged(nameof(Computers));
                IsLoading = false;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ActionStore.CloseWindow();
            }
        });
        OpenAddComputerDialog = new ActionCommand(() =>
        {
            WindowDialogStore.OpenAddComputerWindowDialog();
        });
    }
    public ObservableCollection<Computer> Computers { get; set; }

    public ICommand OpenAddComputerDialog { get; set; }
}