namespace SudInfo.Avalonia.ViewModels.WindowViewModels;
public class TaskWindowViewModel : BaseViewModel
{
    private readonly NavigationService _navigationService;
    private readonly TaskService _taskService;
    private readonly DialogService _dialogService;
    public TaskEntity Task { get; set; } = new();
    public DateTimeOffset ReminderDate { get; set; } = DateTimeOffset.Now;
    public TimeSpan ReminderTime { get; set; } = DateTime.Now.TimeOfDay;
    public TaskWindowViewModel(NavigationService navigationService, TaskService taskService, DialogService dialogService)
    {
        _navigationService = navigationService;
        _taskService = taskService;
        _dialogService = dialogService;
    }
    public async Task AddTask()
    {
        if (!ValidationModel(Task))
            return;
        Task.ReminderTime = new DateTime(ReminderDate.Year, ReminderDate.Month, ReminderDate.Day, ReminderTime.Hours, ReminderTime.Minutes, 0);
        var addTaskResult = await _taskService.AddTask(Task);
        if (!addTaskResult.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", addTaskResult.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }
}