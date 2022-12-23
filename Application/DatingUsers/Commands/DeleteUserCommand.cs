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
            .Where(u => u.ChatId == chatId)
            .ToListAsync();

        var requests = await _context.Requests
            .Where(u => u.ChatId == chatId)
            .ToListAsync();

        if (datingUser != null && tlgUser != null)
        {
            _context.DatingUsers.Remove(datingUser);

            _context.TlgUsers.Remove(tlgUser);

            if (photos != null)
            {
                foreach (var photo in photos)
                {
                    _context.Photos.Remove(photo);
                }
            }

            if (requests != null)
            {
                foreach (var request in requests)
                {
                    _context.Requests.Remove(request);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
