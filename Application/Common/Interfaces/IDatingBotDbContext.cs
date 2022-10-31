namespace Application.Common.Interfaces;

public interface IDatingBotDbContext 
{
    DbSet<DatingUser> DatingUsers { get; } 

    DbSet<TlgUser> TlgUsers { get; }

    DbSet<Photo> Photos { get; }

    DbSet<Request> Requests { get; }

    DbSet<BlockedUser> BlockedUsers { get; }

    Task SaveChangesAsync();
}
