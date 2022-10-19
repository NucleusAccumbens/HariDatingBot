using Application.DatingUsers.Interfaces;

namespace Application.DatingUsers.Queries;

public class CheckUserHasPhotosQuery : ICheckUserHasPhotosQuery
{
    private readonly IDatingBotDbContext _context;

    public CheckUserHasPhotosQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckUserHasPhotosAsync(long chatId)
    {
        var user = await _context.DatingUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (user != null) return user.HasPhotos;

        else return false;
    }
}
