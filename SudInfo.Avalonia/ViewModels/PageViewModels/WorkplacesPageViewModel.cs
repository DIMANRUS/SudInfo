namespace SudInfo.Avalonia.ViewModels.PageViewModels;

public class WorkplacesPageViewModel : BaseRoutableViewModel
{
    [Reactive]
    public IReadOnlyList<User> Users { get; set; }
    public WorkplacesPageViewModel(IUserService userService, IDialogService dialogService)
    {
        Initialized = ReactiveCommand.CreateFromTask(async () =>
        {
            var usersLoadResult = await userService.GetUsersWithComputers();
            if (!usersLoadResult.Success)
            {
                await dialogService.ShowMessageBox("Ошибка", "Ошибка загрузки!", icon: Icon.Error);
                return;
            }
            Users = usersLoadResult.Object;
        });
    }
}