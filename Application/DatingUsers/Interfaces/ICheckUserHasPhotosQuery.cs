namespace Application.DatingUsers.Interfaces;

public interface ICheckUserHasPhotosQuery
{
    Task<bool> CheckUserHasPhotosAsync(long chatId);
}
