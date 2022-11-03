namespace Application.TlgUsers.Interfaces;

public interface IGetUsernameTlgUserQuery
{
    Task<string?> GetUsernameAsync(long chatId);
}
