using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Queries;

public class GetAdminsChatIdQuery : IGetAdminsChatIdQuery
{
    private readonly IDatingBotDbContext _context;

    public GetAdminsChatIdQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<List<long>> GetAdminsChatIdAsync()
    {
        var admins = await _context.TlgUsers
            .Where(u => u.IsAdmin == true)
            .ToListAsync();

        List<long> chatIds = new();

        foreach (var admin in admins) chatIds.Add(admin.ChatId);

        return chatIds;
    }
}
