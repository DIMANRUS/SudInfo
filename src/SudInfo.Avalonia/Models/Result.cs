﻿namespace SudInfo.Avalonia.Models;

public class Result<T> : Result
{
    public T? Object { get; set; }
}
public class Result
{
    public Result(bool success = false, string message = "")
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}