using Domain.Entities;

namespace Application.TlgUsers.Interfaces;

public interface ICreateTlgUserCommand
{
    Task CreateTlgUserAsync(TlgUser tlgUser);
}
