namespace Application.Requests.Interfaces;

public interface IGetRequestQuery
{
    Task GetRequestAsync(long chatId);

    Task<Request> GetRequests(long chatId);
}
