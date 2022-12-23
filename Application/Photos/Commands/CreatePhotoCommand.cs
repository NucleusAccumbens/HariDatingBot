using Application.Photos.Interfaces;

namespace Application.Photos.Commands;

public class CreatePhotoCommand : ICreatePhotoCommand
{
    private readonly IDatingBotDbContext _context;

    public CreatePhotoCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<long> CreatePhotoAsync(Photo photo)
    {
        await _context.Photos
            .AddAsync(photo);

        await _context.SaveChangesAsync();

        return photo.Id;
    }
}
