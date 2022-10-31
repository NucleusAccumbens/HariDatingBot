namespace Application.BlockedUsers.Interfaces;

public interface IBlockDatingUserCommand
{
    Task BlockUserAsync(long chatId, long blockingChatId);

    Task UnlockUserAsync(long chatId, long blockingChatId);
}
