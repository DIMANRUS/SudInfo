namespace SudInfo.Avalonia.Interfaces;
public interface IDialogService
{
    void SetCurrentWindow(Window currentWindow);
    Task<ButtonResult> ShowMessageBox(string title, string text, bool isCLosedWindow = false, ButtonEnum buttonEnum = ButtonEnum.Ok, Icon icon = Icon.None);
}