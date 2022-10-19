namespace Application.Photos.Interfaces;

public interface IGetPhotosQuery
{
    Task<List<Photo>> GetUserPhotosAsync(long chatId);

    Task<Photo?> GetAvatarAsync(long chatId);
}
