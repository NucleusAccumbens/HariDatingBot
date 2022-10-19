using Application.Photos.Interfaces;

namespace Application.Photos.Queries;

public class GetPhotosQuery : IGetPhotosQuery
{
    private readonly IDatingBotDbContext _context;

    public GetPhotosQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<Photo?> GetAvatarAsync(long chatId)
    {
        return await _context.Photos
            .Where(ph => ph.ChatId == chatId)
            .Where(ph => ph.IsAvatar == true)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Photo>> GetUserPhotosAsync(long chatId)
    {
        return await _context.Photos
            .Where(ph => ph.ChatId == chatId)
            .ToListAsync();
    }
}
