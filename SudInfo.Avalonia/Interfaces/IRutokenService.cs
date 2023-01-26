namespace SudInfo.Avalonia.Interfaces;
public interface IRutokenService
{
    #region Get Methods
    Task<Result<List<Rutoken>>> GetRutokens();
    Task<Result<Rutoken>> GetRutokenById(int id);
    #endregion
    Task<Result> AddRutoken(Rutoken rutoken);
    Task<Result> UpdateRutoken(Rutoken rutoken);
    Task<Result> RemoveRutokenById(int id);
}