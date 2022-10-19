using Domain.Entities;

namespace Application.DatingUsers.Interfaces;

public interface IGetDatingUserQuery
{
    Task<DatingUser?> GetDatingUserAsync(long chatId);
}
