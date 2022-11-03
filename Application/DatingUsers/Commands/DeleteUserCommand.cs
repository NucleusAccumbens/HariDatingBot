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

        var photos = await _context.Photos
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        var requests = await _context.Requests
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (datingUser != null && tlgUser != null)
        {
            _context.DatingUsers.Remove(datingUser);

            _context.TlgUsers.Remove(tlgUser);

            if (photos != null) _context.Photos.Remove(photos);

            if (requests != null) _context.Requests.Remove(requests);

            await _context.SaveChangesAsync();
        }
    }
}
