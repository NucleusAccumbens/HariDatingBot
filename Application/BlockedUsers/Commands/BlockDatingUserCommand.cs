using Application.BlockedUsers.Interfaces;

namespace Application.BlockedUsers.Commands;

public class BlockDatingUserCommand : IBlockDatingUserCommand
{
    private readonly IDatingBotDbContext _context;

    public BlockDatingUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task BlockUserAsync(long chatId, long blockingChatId)
    {
        var entity = await _context.BlockedUsers
            .Where(e => e.UserChatId == chatId)
            .Where(e => e.BlockedUserChatId == blockingChatId)
            .SingleOrDefaultAsync();

        if (entity == null)
        {
            await _context.BlockedUsers
                .AddAsync(new BlockedUser() { UserChatId = chatId, BlockedUserChatId = blockingChatId });

            await _context.SaveChangesAsync();
        }

        else entity.IsDeleted = false;
    }

    public async Task UnlockUserAsync(long chatId, long blockingChatId)
    {
        var entity = await _context.BlockedUsers
            .Where(e => e.UserChatId == chatId)
            .Where(e => e.BlockedUserChatId == blockingChatId)
            .SingleOrDefaultAsync();

        if (entity != null)
        {
            entity.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}
