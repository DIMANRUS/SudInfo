using DocumentFormat.OpenXml.Vml.Office;

namespace SudInfo.Avalonia.Services;

public class PhoneService : BaseService<Phone>
{
    #region Ctors

    public PhoneService(SudInfoDatabaseContext context) : base(context) { }

    #endregion

    #region Get methods

    public async Task<Result<Phone>> Get(int id)
    {
        try
        {
            var phone = await context.Phones.AsNoTracking()
                                            .Include(x => x.User)
                                            .FirstAsync(x => x.Id == id);
            return phone == null
                ? throw new Exception("Телефон не найден")
                : new(phone, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<Phone>> Get()
    {
        var phones = await context.Phones.AsNoTracking()
                                         .Include(x => x.User)
                                         .ToListAsync();
        return phones;
    }

    #endregion

    public override async Task<Result> Add(Phone phone)
    {
        try
        {
            if (phone.User != null)
            {
                phone.User = await context.Users.AsNoTracking()
                                                .FirstAsync(x => x.Id == phone.User.Id);
            }
            context.Entry(phone).State = EntityState.Added;
            await context.AddAsync(phone);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }

    public async Task<Result> Remove(int id)
    {
        try
        {
            var phone = await context.Phones.AsNoTracking()
                                            .FirstAsync(x => x.Id == id);
            context.Entry(phone).State = EntityState.Deleted;
            context.Remove(phone);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }
}