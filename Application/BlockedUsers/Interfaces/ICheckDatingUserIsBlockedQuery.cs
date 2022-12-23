namespace Application.BlockedUsers.Interfaces;

public interface ICheckDatingUserIsBlockedQuery
{
    Task<bool> CheckDatingUserIsBlockedAsync(long chatId, long requestedDialogChatId);
}
