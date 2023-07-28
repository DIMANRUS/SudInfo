namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class TasksPageViewModel : BaseRoutableViewModel
{
    #region Services

    private readonly NavigationService _navigationService;

    private readonly TaskService _taskService;

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

    public TasksPageViewModel(NavigationService navigationService, TaskService taskService)
    {
        #region Serives Initialization

        _navigationService = navigationService;
        _taskService = taskService;

        #endregion

        _eventHandlerClosedWindowDialog = async (s, e) => await LoadTasks();

        CompleteTaskCommand = ReactiveCommand.Create<int>(async (int id) =>
        {
            var completeTaskResult = await _taskService.CompleteTask(id);
            if (!completeTaskResult.Success)
            {
                await DialogService.ShowErrorMessageBox(completeTaskResult.Message);
                return;
            }
            await LoadTasks();
        });
    }

    #endregion

    #region Public Methods

    public async Task LoadTasks()
    {
        var tasksResult = await _taskService.Get();
        Tasks = tasksResult;
    }

    public async Task OpenAddTaskWindow()
    {
        await _navigationService.ShowTaskWindowDialog(_eventHandlerClosedWindowDialog);
    }

    #endregion
}