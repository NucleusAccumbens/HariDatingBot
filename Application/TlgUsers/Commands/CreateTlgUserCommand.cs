using Application.TlgUsers.Interfaces;
using Domain.Entities;

namespace Application.TlgUsers.Commands;

public class CreateTlgUserCommand : ICreateTlgUserCommand
{
    private readonly IDatingBotDbContext _context;

    public CreateTlgUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateTlgUserAsync(TlgUser tlgUser)
    {
        await _context.TlgUsers
            .AddAsync(tlgUser);

        await _context.SaveChangesAsync();
    }
}
