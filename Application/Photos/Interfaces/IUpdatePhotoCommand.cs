namespace Application.Photos.Interfaces;

public interface IUpdatePhotoCommand
{
    Task<long> UpdarePhotoIsVerifideAsync(long photoId);

    Task UpdatePhotoIsAvatar(long photoId);
}
