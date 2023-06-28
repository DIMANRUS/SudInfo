namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class TasksPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly TaskService _taskService;
    private readonly DialogService _dialogService;
    private readonly NavigationService _navigationService;

    #endregion

    #region Collections

    [Reactive]
    public ObservableCollection<TaskEntity>? Tasks { get; set; }
    private IReadOnlyList<TaskEntity>? TasksFromDatabase { get; set; }

    #endregion

    #region Private Variables

    private readonly EventHandler _eventHandlerClosedWindowDialog;

    #endregion

    #region Properties

    [Reactive]
    public string SearchText { get; set; } = string.Empty;

    [Reactive]
    public TaskEntity? SelectedTask { get; set; }

    #endregion

    #region Initialization

    public TasksPageViewModel(
        NavigationService navigationService,
        TaskService taskService,
        DialogService dialogService)
    {
        #region Serives Initialization
        _dialogService = dialogService;
        _taskService = taskService;
        _navigationService = navigationService;
        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) => await LoadTasks();
    }

    #endregion

    #region Public Methods

    public async Task LoadTasks()
    {
        var tasksResult = await TaskService.GetTasks();
        Tasks = new(tasksResult);
        TasksFromDatabase = Tasks;
    }
    public void SearchBoxKeyUp()
    {
        if (string.IsNullOrEmpty(SearchText))
        {
            Tasks = new(TasksFromDatabase);
            return;
        }
        Tasks = new(TasksFromDatabase.Where(x => x.Description.ToLower().Contains(SearchText.ToLower())));
    }
    public async Task CompleteTask()
    {
        if (SelectedTask == null)
            return;
        var completeTaskResult = await TaskService.CompleteTask(SelectedTask.Id);
        if (!completeTaskResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {completeTaskResult.Message}", icon: Icon.Error);
            return;
        }
        await LoadTasks();
    }
    public async Task OpenAddTaskWindow()
    {
        await _navigationService.ShowTaskWindowDialog(_eventHandlerClosedWindowDialog);
    }
    #endregion
}