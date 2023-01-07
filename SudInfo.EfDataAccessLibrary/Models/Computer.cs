﻿namespace SudInfo.EFDataAccessLibrary.Models;
public class Computer : BaseModel
{
    [StringLength(15)]
    public string Ip { get; set; } = string.Empty;
    public OS OS { get; set; } = OS.Windows7;
    [StringLength(40)]
    public string CPU { get; set; } = string.Empty;
    [StringLength(40)]
    public string? GPU { get; set; }
    public int ROM { get; set; }
    public byte RAM { get; set; }
    [StringLength(20)]
    public string SerialNumber { get; set; } = string.Empty;
    [StringLength(20)]
    public string InventarNumber { get; set; } = string.Empty;
    public Employee? Employee { get; set; }
    public bool IsDecommissioned { get; set; }
}

public enum OS
{
    Windows7, Windows10, Linux, ESXi, WindowsXP, WindowsServer2003, WindowsServer2008, WindowsServer2012, WindowsServer2016, AstraLinux
}