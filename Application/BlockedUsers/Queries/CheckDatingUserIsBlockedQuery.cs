using Application.BlockedUsers.Interfaces;

namespace Application.BlockedUsers.Queries;

public class CheckDatingUserIsBlockedQuery : ICheckDatingUserIsBlockedQuery
{
    private readonly IDatingBotDbContext _context;

    public CheckDatingUserIsBlockedQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckDatingUserIsBlockedAsync(long chatId, long requestedDialogChatId)
    {
        var entity = await _context.BlockedUsers
            .Where(u => u.UserChatId == requestedDialogChatId)
            .Where(u => u.BlockedUserChatId == chatId)
            .Where(u => u.IsDeleted == false)
            .SingleOrDefaultAsync();

        if (entity == null || entity.IsDeleted == true) return false;

        else return true;
    }
}
