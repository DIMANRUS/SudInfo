namespace SudInfo.Avalonia.Services;
public class BaseService
{
    public virtual async Task<Result> Update<T>(T entity)
    {
        try
        {
            if (entity == null)
                throw new("Entity null");
            using SudInfoDbContext applicationDBContext = new();
            applicationDBContext.Update(entity);
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
    public virtual async Task<Result> Add<T>(T entity)
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