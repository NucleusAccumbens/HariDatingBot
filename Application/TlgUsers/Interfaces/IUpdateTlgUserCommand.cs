namespace Application.TlgUsers.Interfaces;

public interface IUpdateTlgUserCommand
{
    Task UpdateTlgUsernameAsync(long chatId, string username);
}
