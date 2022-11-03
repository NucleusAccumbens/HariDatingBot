using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Queries;

public class GetUsernameTlgUserQuery : IGetUsernameTlgUserQuery
{
    private readonly IDatingBotDbContext _context;

    public GetUsernameTlgUserQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<string?> GetUsernameAsync(long chatId)
    {
        return await _context.TlgUsers
            .Where(u => u.ChatId == chatId)
            .Select(u => new String(u.Username))
            .SingleOrDefaultAsync();
    }
}
