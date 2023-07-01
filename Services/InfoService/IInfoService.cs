namespace server.Services.InfoService
{

    public interface IInfoService
    {
        Task<Info> CreateInfo(int TelephoneNumber, InfoDto dto);
        Task<List<Info>> GetAllInfos();
        Task<Info?> UpdateInfo(int id, InfoDto dto);
        Task<Info?> DeleteInfo(int id);
        Task<int> Role(int infoId, int userId);
        Task<bool> HasLaughed(int infoId, int userId);
    }
}