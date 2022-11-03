using Application.DatingUsers.Interfaces;

namespace Application.DatingUsers.Commands;

public class DeleteUserCommand : IDeleteUserCommand
{
    private readonly IDatingBotDbContext _context;

    public DeleteUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task DeleteDaytingUserAsync(long chatId)
    {
        var datingUser = await _context.DatingUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        var tlgUser = await _context.TlgUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (datingUser != null && tlgUser != null)
        {
            _context.DatingUsers.Remove(datingUser);

            _context.TlgUsers.Remove(tlgUser);

            await _context.SaveChangesAsync();
        }
    }
}
