using Domain.Entities;

namespace Application.DatingUsers.Interfaces;

public interface ICreateDatingUserCommand
{
    Task CreateDatingUserAsync(DatingUser datingUser);
}
