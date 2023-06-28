namespace SudInfo.Avalonia.Services;

public class ExcelService
{
    public static async Task CreateExcelTableFromEntity<T>(IReadOnlyCollection<T> entity)
    {
        if (App.MainWindow == null)
            return;
        using XLWorkbook wb = new();

        var ws = wb.Worksheets.Add(nameof(entity));
        ws.Cell(1, 1).InsertTable(entity);
        ws.Columns().AdjustToContents();
        SaveFileDialog saveFileDialog = new()
        {
            Title = "Выберите путь сохранения",
            InitialFileName = "Table.xlsx",
            Filters = new()
            {
                new()
                {
                    Name = "Excel",
                    Extensions = new(){"xlsx"}
                }
            }
        };
        var dialogResult = await saveFileDialog.ShowAsync(App.MainWindow);
        wb.SaveAs(dialogResult);
    }
}