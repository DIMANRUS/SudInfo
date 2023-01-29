﻿namespace SudInfo.EfDataAccessLibrary.Models;
public class Printer : BaseModel
{
    [StringLength(40)]
    public string Name { get; set; } = string.Empty;
    public PrinterType Type { get; set; } = PrinterType.Принтер;
    [StringLength(maximumLength: 12)]
    public string? Ip { get; set; }
    public int Cabinet { get; set; }
    public int YearRelease { get; set; }
    public bool IsDecommissioned { get; set; }
    public User? User { get; set; }
}

public enum PrinterType
{
    Принтер, МФУ
}