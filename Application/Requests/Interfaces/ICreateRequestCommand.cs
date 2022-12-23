namespace Application.Requests.Interfaces;

public interface ICreateRequestCommand
{
    Task CreateRequestAsync(Request request);
}
