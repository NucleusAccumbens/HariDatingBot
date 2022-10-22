using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence;

public class DatingBotDbCotext : DbContext, IDatingBotDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    
    public DatingBotDbCotext(DbContextOptions<DatingBotDbCotext> options, 
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<DatingUser> DatingUsers => Set<DatingUser>();

    public DbSet<TlgUser> TlgUsers => Set<TlgUser>();

    public DbSet<Photo> Photos => Set<Photo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<TlgUser>()
            .HasData(new TlgUser()
            {
                Id = 1,
                ChatId = 444343256,
                Username = "noncredistka",
                IsAdmin = true
            }); 

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }

}

