﻿namespace SudInfo.Avalonia.Services;
public class RutokenService : BaseService
{
    #region Get Methods
    public async Task<Result<Rutoken>> GetRutokenById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var rutoken = await applicationDBContext.Rutokens.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (rutoken == null)
                throw new Exception("ЭЦП не найден");
            return new()
            {
                Success = true,
                Object = rutoken
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    public async Task<Result<List<Rutoken>>> GetRutokens()
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var rutokens = await applicationDBContext.Rutokens.Include(x => x.User).ToListAsync();
            return new()
            {
                Success = true,
                Object = rutokens
            };
        }
        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = ex.Message
            };
        }
    }
    #endregion

    public async Task<Result> RemoveRutokenById(int id)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            var rutoken = await applicationDBContext.Rutokens.FirstOrDefaultAsync(x => x.Id == id);
            if (rutoken == null)
                throw new Exception("ЭЦП не найден");
            applicationDBContext.Rutokens.Remove(rutoken);
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
                Success = false,
                Message = ex.Message
            };
        }
    }
    public async Task<Result> AddRutoken(Rutoken rutoken)
    {
        try
        {
            using SudInfoDbContext applicationDBContext = new();
            if (rutoken.User != null)
            {
                rutoken.User = await applicationDBContext.Users.SingleOrDefaultAsync(x => x.Id == rutoken.User.Id);
            }
            await applicationDBContext.Rutokens.AddAsync(rutoken);
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
                Success = false,
                Message = ex.Message
            };
        }
    }
}