using Application.TlgUsers.Interfaces;

namespace Application.TlgUsers.Queries;

public class CheckUsernameIsValidQuery : ICheckUsernameIsValidQuery
{
    private readonly IDatingBotDbContext _context;

    public CheckUsernameIsValidQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckUsernameIsValidAsync(long chatId, string username)
    {
        string? savedUsername = await _context.TlgUsers
            .Where(u => u.ChatId == chatId)
            .Select(u => new String(u.Username))
            .SingleOrDefaultAsync();

        if (savedUsername == null || savedUsername != username)
        {
            return false;
        }

        else return true;
    }
}
