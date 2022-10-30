using Application.Requests.Interfaces;

namespace Application.Requests.Commands;

public class CreateRequestCommand : ICreateRequestCommand
{
    private readonly IDatingBotDbContext _context; 

    public CreateRequestCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task CreateRequestAsync(Request request)
    {
        await _context.Requests
                .AddAsync(request);

        await _context.SaveChangesAsync();
    }
}
