﻿namespace SudInfo.Avalonia.Services;
public class DialogService : IDialogService
{
    #region Private Variables
    private Window _currentWindow;
    #endregion

    #region Constants
    private const int widthDialog = 500;
    #endregion

    #region Setters
    public void SetCurrentWindow(Window currentWindow) =>
        _currentWindow = currentWindow;
    #endregion

    #region MessageBoxes Shows
    public async Task<ButtonResult> ShowMessageBox(string title, string text, bool isCLosedWindow = false, ButtonEnum buttonEnum = ButtonEnum.Ok, Icon icon = Icon.Info)
    {
        var result = await MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams
        {
            ContentTitle = title,
            ContentMessage = text,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            CanResize = false,
            MaxWidth = widthDialog,
            MinWidth = widthDialog,
            SizeToContent = SizeToContent.WidthAndHeight,
            ShowInCenter = true,
            ButtonDefinitions = buttonEnum,
            Icon = icon,
            Topmost = true
        }).ShowDialog(_currentWindow);
        if (isCLosedWindow)
            _currentWindow.Close();
        return result;
    }
    #endregion;
}