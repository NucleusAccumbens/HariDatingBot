namespace Application.DatingUsers.Interfaces;

public interface IGetDatingUserRequestsQuery
{
    Task<List<Request>> GetRequestsAsync(long chatId);
}
