namespace SudInfo.Avalonia.Services;

public class DialogService
{
    public static async Task ShowErrorMessageBox(string message)
    {
        await MessageBoxManager.GetMessageBoxStandard("Ошибка",
                                                      message,
                                                      icon: Icon.Error)
                               .ShowAsync();
    }

    public static async Task<ButtonResult> ShowQuestionMessageBox(string message)
    {
        ButtonResult result = await MessageBoxManager.GetMessageBoxStandard("Сообщение",
                                                                            message,
                                                                            ButtonEnum.YesNo,
                                                                            icon: Icon.Question)
                                                     .ShowAsync();
        return result;
    }
}