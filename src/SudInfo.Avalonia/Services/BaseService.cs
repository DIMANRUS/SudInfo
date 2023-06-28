namespace SudInfo.Avalonia.Services;

public class BaseService
{
    public virtual async Task<Result> Update<T>(params T[] entity) where T : class
    {
        try
        {
            if (entity == null)
                throw new("Entity null");
            using SudInfoDbContext applicationDBContext = new();
            applicationDBContext.UpdateRange(entity);
            await applicationDBContext.SaveChangesAsync();
            return new()
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
    public virtual async Task<Result> Add<T>(T entity) where T : class
    {
        try
        {
            if (entity == null)
                throw new("Entity null");
            using SudInfoDbContext applicationDBContext = new();
            await applicationDBContext.AddAsync(entity);
            await applicationDBContext.SaveChangesAsync();
            return new()
            {
                Success = true
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
}