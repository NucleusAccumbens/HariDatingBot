using Application.DatingUsers.Interfaces;
using Domain.Entities;

namespace Application.DatingUsers.Commands;

public class CreateDatingUserCommand : ICreateDatingUserCommand
{
    private readonly IDatingBotDbContext _context;

    public CreateDatingUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateDatingUserAsync(DatingUser datingUser)
    {
        await _context.DatingUsers
            .AddAsync(datingUser);

        await _context.SaveChangesAsync();
    }
}
