using Application.Photos.Interfaces;

namespace Application.Photos.Commands;

public class DeletePhotoCommand : IDeletePhotoCommand
{
    private readonly IDatingBotDbContext _context;

    public DeletePhotoCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<long> DeletePhotoAsync(long photoId)
    {
        var photo = await _context.Photos
            .SingleOrDefaultAsync(p => p.Id == photoId);

        if (photo != null)
        {
            _context.Photos.Remove(photo);

            await _context.SaveChangesAsync();

            return photo.ChatId;
        }

        return 0;
    }
}
