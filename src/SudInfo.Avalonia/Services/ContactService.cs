namespace SudInfo.Avalonia.Services;

public class ContactService : BaseService
{
    #region Get Methods

    public static async Task<IReadOnlyCollection<Contact>> GetContacts()
    {
        using SudInfoDbContext context = new();
        var contacts = await context.Contacts.AsNoTracking().ToListAsync();
        return contacts;
    }
    public static async Task<Result<Contact>> GetContact(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var server = await applicationDBContext.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Contact not Found");
            return new()
            {
                Success = true,
                Object = server
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

    public static async Task<Result> RemoveContact(int id)
    {
        try
        {
            using SudInfoDbContext context = new();
            var entity = await context.Contacts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id) ?? throw new Exception("Contact not found");
            context.Contacts.Remove(entity);
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
}