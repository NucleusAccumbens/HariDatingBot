using Application.DatingUsers.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace Application.DatingUsers.Commands;

public class UpdateDatingUserCommand : IUpdateDatingUserCommand
{
    private readonly IDatingBotDbContext _context;

    public UpdateDatingUserCommand(IDatingBotDbContext context)
    {
        _context = context;
    }

    public async Task AddRequestAsync(long chatId, Request request)
    {
        var entity = await _context.DatingUsers
                .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity != null)
        {
            entity.Requests.Add(request);

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateDatingUserHasAPhotoAsync(long chatId)
    {
        var entity = await _context.DatingUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity != null)
        {
            entity.HasPhotos = true;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateUserAboutAsync(long chatId, string text)
    {
        var entity = await _context.DatingUsers
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity != null)
        {
            entity.OtherInfo = text;

            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool?> UpdateUserEthicsAsync(long chatId, string filedName)
    {
        var entity = await _context.DatingUsers          
            .SingleOrDefaultAsync(u => u.ChatId == chatId);

        if (entity != null)
        {
            Type type = entity.GetType();

            var property = type.GetProperty(filedName);

            bool? value = (bool?)property?.GetValue(entity);

            property?.SetValue(entity, !value);

            await _context.SaveChangesAsync();

            return (bool?)property?.GetValue(entity);
        }

        return null;
    }
}
