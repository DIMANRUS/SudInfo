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
    public IReadOnlyCollection<TaskEntity>? Tasks { get; set; }

    #endregion

    #region Private Variables

    private readonly EventHandler _eventHandlerClosedWindowDialog;

    #endregion

    #region Commands

    public ReactiveCommand<int, Unit> CompleteTaskCommand { get; init; }

    #endregion

    #region Ctors

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

        CompleteTaskCommand = ReactiveCommand.Create<int>(async (int id) =>
        {
            var completeTaskResult = await TaskService.CompleteTask(id);
            if (!completeTaskResult.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения данных! Ошибка: {completeTaskResult.Message}", icon: Icon.Error);
                return;
            }
            await LoadTasks();
        });
    }

    #endregion

    #region Public Methods

    public async Task LoadTasks()
    {
        var tasksResult = await TaskService.GetTasks();
        Tasks = tasksResult;
    }

    public async Task OpenAddTaskWindow()
    {
        await _navigationService.ShowTaskWindowDialog(_eventHandlerClosedWindowDialog);
    }

    #endregion
}