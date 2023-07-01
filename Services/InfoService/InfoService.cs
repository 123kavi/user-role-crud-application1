using server.Data;

namespace server.Services.InfoService;

public class InfoService : IInfoService
{
    private readonly InfoContext _infoContext;
    private readonly UserInfoRoleContext _roleContext;
    public InfoService(InfoContext infoContext, UserInfoRoleContext roleContext)
    {
        _infoContext = infoContext;
        _roleContext = roleContext;
    }
    public async Task<Info> CreateInfo(int TelephoneNumber, InfoDto dto)
    {
        var newInfo = new Info()
        {
            UserInformation = dto.UserInformation,
            UserAddress = dto.UserAddress,
            TelephoneNumber = TelephoneNumber,
        };
        _infoContext.Infos.Add(newInfo);
        await _infoContext.SaveChangesAsync();

        return newInfo;
    }
    public async Task<List<Info>> GetAllInfos()
    {
        var infos = await _infoContext.Infos.ToListAsync();
        return infos;
    }

    public async Task<Info?> DeleteInfo(int id)
    {
        var Info = await _infoContext.Infos.FindAsync(id);
        if (Info is null) return null;

        _infoContext.Infos.Remove(Info);
        await _infoContext.SaveChangesAsync();

        return Info;
    }

    public async Task<Info?> UpdateInfo(int id, InfoDto dto)
    {
        var Info = await _infoContext.Infos.FindAsync(id);
        if (Info is null) return null;

        Info.UserInformation = dto.UserInformation;
        Info.UserAddress = dto.UserAddress;

        await _infoContext.SaveChangesAsync();

        return Info;
    }

    public async Task<int> Role(int infoId, int userId)
    {
        var Info = await _infoContext.Infos.FindAsync(infoId);
        if (Info is null) return -1;
        //// check if user laughed already
        var hasLaughed = await _roleContext.UserInfoRole.FirstOrDefaultAsync(e => e.UserId == userId && e.InfoId == infoId);
        if (hasLaughed is null)
        {
            Info.Role += 1;
            var relation = new UserInfoRole()
            {
                UserId = userId,
                InfoId = infoId,
            };
            await _roleContext.AddAsync(relation);
        }
        else
        {
            Info.Role -= 1;
            _roleContext.Remove(hasLaughed);
        }
        await _roleContext.SaveChangesAsync();
        await _infoContext.SaveChangesAsync();
        return Info.Role;
    }
    public async Task<bool> HasLaughed(int infoId, int userId)
    {
        var hasLaughed = await _roleContext.UserInfoRole.FirstOrDefaultAsync(e => e.UserId == userId && e.InfoId == infoId);
        if (hasLaughed is null) return false;
        return true;
    }
}