using Application.DatingUsers.Interfaces;
using Domain.Entities;
using Microsoft.VisualBasic;

namespace Application.DatingUsers.Queries;

public class GetDatingUserQuery : IGetDatingUserQuery
{
    private readonly IDatingBotDbContext _context;

    public GetDatingUserQuery(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<DatingUser?> GetDatingUserAsync(long chatId)
    {
        return await _context.DatingUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);
    }
}
