namespace SudInfo.Avalonia.ViewModels.WindowViewModels;

public class AppWindowViewModel : BaseViewModel
{
    #region Services

    private readonly DialogService _dialogService;

    #endregion

    #region Properties

    [Reactive]
    public AppEntity AppEntity { get; set; } = new();

    [Reactive]
    public string SaveButtonText { get; private set; } = "Добавить приложение";

    [Reactive]
    public bool ButtonIsVisible { get; private set; }

    #endregion

    #region Collections

    [Reactive]
    public IReadOnlyCollection<Computer>? Computers { get; set; }

    #endregion

    #region Private Fields

    private WindowType _windowType;

    #endregion

    #region Commands

    public ReactiveCommand<int, Unit> SelectionComputerChanged { get; init; }

    #endregion

    #region Ctors

    public AppWindowViewModel() { }

    public AppWindowViewModel(DialogService dialogService)
    {
        _dialogService = dialogService;

        SelectionComputerChanged = ReactiveCommand.Create((int id) =>
        {
            var computerExist = AppEntity.Computers.Any(x => x.Id == id);
            if (computerExist)
            {
                AppEntity.Computers.Remove(AppEntity.Computers.Single(x => x.Id == id));
                return;
            }
            AppEntity.Computers.Add(Computers!.Single(x => x.Id == id));
        });
    }
    #endregion

    #region Public methods

    public async Task SaveApp()
    {
        if (!ValidationModel(AppEntity))
            return;
        Result result = _windowType switch
        {
            WindowType.Add => await AppService.AddApp(AppEntity),
            _ => await AppService.UpdateApp(AppEntity)
        };
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", result.Message, icon: Icon.Error);
            return;
        }
        await _dialogService.ShowMessageBox("Сообщение", "Успешно!", true, icon: Icon.Success);
    }

    public async Task InitializationData(WindowType windowType, int? id = null)
    {
        _windowType = windowType;
        if (windowType != WindowType.View)
        {
            ButtonIsVisible = true;
        }

        if (id != null)
        {
            if (windowType != WindowType.View)
            {
                ButtonIsVisible = true;
                SaveButtonText = "Сохранить приложение";
            }

            var result = await AppService.GetApp(id.GetValueOrDefault());
            if (!result.Success)
            {
                await _dialogService.ShowMessageBox("Ошибка", $"Ошибка получения компьютера! Ошибка: {result.Message}",
                    true, icon: Icon.Error);
                return;
            }
            AppEntity = result.Object;
        }
        Computers = await ComputerService.GetComputers();
        if (windowType == WindowType.Edit)
        {
            foreach (var computer in Computers)
            {
                computer.IsSelected = AppEntity!.Computers.Any(x => x.Id == computer.Id);
            }
        }
    }

    #endregion
}