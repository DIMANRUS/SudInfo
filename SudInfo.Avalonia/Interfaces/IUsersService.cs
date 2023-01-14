namespace SudInfo.Avalonia.Interfaces;
public interface IUsersService
{
    #region Get Methods
    Task<TaskResult<List<User>>> GetUsers();
    Task<TaskResult<User>> GetUserById(int userId);
    #endregion
    Task<TaskResult> AddUser(User user);
    Task<TaskResult> SaveUser(User user);
    Task<TaskResult> RemoveUserById(int userId);
}