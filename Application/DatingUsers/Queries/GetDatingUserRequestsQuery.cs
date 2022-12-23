using Application.DatingUsers.Interfaces;

namespace Application.DatingUsers.Queries;

public class GetDatingUserRequestsQuery : IGetDatingUserRequestsQuery
{
    private readonly IDatingBotDbContext _context;

    public GetDatingUserRequestsQuery(IDatingBotDbContext datingBotDbContext)
    {
        _context = datingBotDbContext;
    }

    public async Task<List<Request>> GetRequestsAsync(long chatId)
    {
        return await _context.Requests
            .Include(e => e.DatingUser)
            .Where(e => e.ChatId == chatId)
            .ToListAsync();
    }
}
