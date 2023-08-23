namespace SudInfo.Avalonia.Services;

public class BaseService<T> where T : class
{
    #region Private variables

    protected readonly SudInfoDatabaseContext context;

    #endregion

    #region Ctors

    public BaseService(SudInfoDatabaseContext context)
    {
        this.context = context;
    }

    #endregion

    #region Methods

    public virtual async Task<Result> Update(params T[] entity)
    {
        try
        {
            if (entity == null)
                throw new("Entity null");
            foreach (var item in entity)
                context.Entry(item).State = EntityState.Modified;
            context.UpdateRange(entity);
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

    public virtual async Task<Result> Add(T entity)
    {
        try
        {
            if (entity == null)
                throw new("Entity null");
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