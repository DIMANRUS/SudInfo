namespace SudInfo.Avalonia.Models;
public class Result<T> where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Object { get; set; }
}

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}