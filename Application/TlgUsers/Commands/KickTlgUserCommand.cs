using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Commands;

public class KickTlgUserCommand : IKickTlgUserCommand
{
    private readonly IDatingBotDbContext _context;

    public KickTlgUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckTlgUserIsKicked(long? chatId)
    {
        if (chatId == null) return true;

        var tlgUser = await _context.TlgUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (tlgUser != null)
        {
            return tlgUser.IsKicked;
        }

        return false;
    }

    public async Task ManageTlgUserKickingAsync(long chatId)
    {
        var tlgUser = await _context.TlgUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        var datingUser = await _context.DatingUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (tlgUser != null && datingUser != null)
        {
            tlgUser.IsKicked = !tlgUser.IsKicked;

            datingUser.IsKicked = !datingUser.IsKicked;

            await _context.SaveChangesAsync();
        }
    }

}
