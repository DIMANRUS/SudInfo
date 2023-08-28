﻿namespace SudInfo.Avalonia.Services;

public class PeripheryService : BaseService<Periphery>
{
    #region Ctors

    public PeripheryService(SudInfoDatabaseContext context) : base(context) { }

    #endregion

    #region Get Methods

    public async Task<Result<Periphery>> Get(int id)
    {
        try
        {
            var periphery = await context.Peripheries.Include(x => x.Computer)
                                                     .ThenInclude(x => x.User)
                                                     .FirstAsync(x => x.Id == id);
            return new(periphery, true);
        }
        catch (Exception ex)
        {
            return new(null, message: ex.Message);
        }
    }

    public async Task<IReadOnlyCollection<Periphery>> Get()
    {

        var peripheries = await context.Peripheries.Include(x => x.Computer)
                                                   .ThenInclude(x => x.User)
                                                   .ToListAsync();
        return peripheries;
    }

    #endregion

    public override async Task<Result> Add(Periphery periphery)
    {
        try
        {
            if (periphery.Computer != null)
            {
                periphery.Computer = await context.Computers.AsNoTracking()
                                                            .FirstAsync(x => x.Id == periphery.Computer.Id);
            }
            context.Entry(periphery).State = EntityState.Added;
            await context.Peripheries.AddAsync(periphery);
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
            var periphery = await context.Peripheries.AsNoTracking()
                                                     .FirstAsync(x => x.Id == id);
            context.Entry(periphery).State = EntityState.Deleted;
            context.Peripheries.Remove(periphery);
            await context.SaveChangesAsync();
            return new(true);
        }
        catch (Exception ex)
        {
            return new(message: ex.Message);
        }
    }

    public override async Task<Result> Update(Periphery periphery)
    {
        try
        {
            var entity = await context.Peripheries.Include(x => x.Computer)
                                                           .FirstAsync(x => x.Id == periphery.Id);
            entity.Computer = periphery.Computer;
            await context.SaveChangesAsync();
            return new(true);
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