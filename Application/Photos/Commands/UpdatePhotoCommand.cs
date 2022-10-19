using Application.Photos.Interfaces;

namespace Application.Photos.Commands;

public class UpdatePhotoCommand : IUpdatePhotoCommand
{
    private readonly IDatingBotDbContext _context;

    public UpdatePhotoCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task<long> UpdarePhotoIsVerifideAsync(long photoId)
    {
        var photo = await _context.Photos
            .SingleOrDefaultAsync(ph => ph.Id == photoId);

        if (photo != null)
        {
            photo.IsVerified = true;

            await _context.SaveChangesAsync();

            return photo.ChatId;
        }

        return 0;
    }

    public Task UpdatePhotoIsAvatar(long photoId)
    {
        throw new NotImplementedException();
    }
}
