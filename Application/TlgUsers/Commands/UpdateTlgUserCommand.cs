using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Commands;

public class UpdateTlgUserCommand : IUpdateTlgUserCommand
{
    private readonly IDatingBotDbContext _context;

    public UpdateTlgUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task UpdateTlgUsernameAsync(long chatId, string username)
    {
        var tlgUser = await _context.TlgUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (tlgUser != null)
        {
            tlgUser.Username = username;

            await _context.SaveChangesAsync();
        }
    }
}
