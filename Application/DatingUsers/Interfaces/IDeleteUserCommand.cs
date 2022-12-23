namespace Application.DatingUsers.Interfaces;

public interface IDeleteUserCommand
{
    Task DeleteDaytingUserAsync(long chatId);
}
