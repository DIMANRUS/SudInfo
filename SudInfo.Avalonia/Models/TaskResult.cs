namespace SudInfo.Avalonia.Models;
public class TaskResult<T> where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Result { get; set; }
}

public class TaskResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}