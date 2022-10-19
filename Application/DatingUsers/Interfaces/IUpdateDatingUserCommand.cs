namespace Application.DatingUsers.Interfaces;

public interface IUpdateDatingUserCommand
{
    Task UpdateUserAboutAsync(long chatId, string text);

    Task<bool?> UpdateUserEthicsAsync(long chatId, string filedName);

    Task UpdateDatingUserHasAPhotoAsync(long chatId);

}
