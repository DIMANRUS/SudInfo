using System.Threading.Tasks;

namespace SudInfo.WPF.ViewModels;
public class ComputersPageViewModel : BaseViewModel
{
    public ComputersPageViewModel()
    {
        Loaded = new ActionCommand(() =>
        {
            try
            {
                LoadComputers();
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
            IsLoading = true;
            LoadComputers();
            IsLoading = false;
        });
    }
    public ObservableCollection<Computer> Computers { get; set; }

    public ICommand OpenAddComputerDialog { get; set; }

    private ICollection<Computer> LoadComputers()
    {
        using ApplicationDBContext context = new();
        return Computers = new(context.Computers);
    }
}