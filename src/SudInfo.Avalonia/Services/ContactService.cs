namespace SudInfo.Avalonia.Services;

public class ContactService : BaseService<Contact>
{
    #region Ctors

    public ContactService(SudInfoDatabaseContext context) : base(context)
    {
    }

    #endregion

    #region Get Methods

    public async Task<IReadOnlyCollection<Contact>> Get()
    {
        var contacts = await context.Contacts.AsNoTracking()
                                             .ToListAsync();
        return contacts;
    }

    public async Task<Result<Contact>> Get(int id)
    {
        try
        {
            var entity = await context.Contacts.FirstAsync(x => x.Id == id);
            return new(entity, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    #endregion

    public async Task<Result> Remove(int id)
    {
        try
        {
            var entity = await context.Contacts.AsNoTracking()
                                               .FirstAsync(x => x.Id == id);
            context.Entry(entity).State = EntityState.Deleted;
            context.Contacts.Remove(entity);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}