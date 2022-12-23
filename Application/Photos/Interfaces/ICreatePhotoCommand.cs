namespace Application.Photos.Interfaces;

public interface ICreatePhotoCommand
{
    Task<long> CreatePhotoAsync(Photo photo);
}
