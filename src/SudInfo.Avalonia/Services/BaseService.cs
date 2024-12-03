namespace SudInfo.Avalonia.Services;

public class BaseService<T>(SudInfoDatabaseContext context) where T : class
{
    #region Private variables

    protected readonly SudInfoDatabaseContext context = context;

    #endregion
    #region Ctors

    #endregion

    #region Methods

    public virtual async Task<Result> Update(T entity)
    {
        try
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Update(entity);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (DbUpdateException ex)
        {
            return new()
            {
                Message = ex.InnerException.Message
            };
        }
    }

    public virtual async Task<Result> Add(T entity)
    {
        try
        {
            context.Entry(entity).State = EntityState.Added;
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
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

    #endregion
}