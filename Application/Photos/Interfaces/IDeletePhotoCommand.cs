namespace Application.Photos.Interfaces;

public interface IDeletePhotoCommand
{
    Task<long> DeletePhotoAsync(long photoId);
}
