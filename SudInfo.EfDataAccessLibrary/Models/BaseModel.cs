﻿namespace SudInfo.EFDataAccessLibrary.Models;
public abstract class BaseModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}