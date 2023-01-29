﻿namespace SudInfo.Avalonia.Interfaces;
public interface IUserService
{
    #region Get Methods
    Task<Result<List<User>>> GetUsers();
    Task<Result<User>> GetUserById(int userId);
    #endregion
    Task<Result> AddUser(User user);
    Task<Result> UpdateUser(User user);
    Task<Result> RemoveUserById(int userId);
}