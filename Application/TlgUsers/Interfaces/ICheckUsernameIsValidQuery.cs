namespace Application.TlgUsers.Interfaces;

public interface ICheckUsernameIsValidQuery
{
    Task<bool> CheckUsernameIsValidAsync(long chatId, string username);
}
