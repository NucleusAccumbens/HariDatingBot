namespace Application.TlgUsers.Interfaces;

public interface IGetAdminsChatIdQuery
{
    Task<List<long>> GetAdminsChatIdAsync();
}
