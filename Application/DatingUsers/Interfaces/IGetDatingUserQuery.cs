using Domain.Entities;

namespace Application.DatingUsers.Interfaces;

public interface IGetDatingUserQuery
{
    Task<DatingUser?> GetDatingUserAsync(long chatId);

    Task<List<DatingUser>> GetAllDatingUsersAsync();

    Task<List<DatingUser>> GetAllDatingUsersAsync(long chatId);
}
