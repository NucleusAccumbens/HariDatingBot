using EblanistsDatingBot.Common.Interfaces;
using EblanistsDatingBot.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EblanistsDatingBot.Common.Services;

public class MemoryCachService : IMemoryCachService
{
    private readonly IMemoryCache _memoryCach;

    public MemoryCachService(IMemoryCache memoryCache)
    {
        _memoryCach = memoryCache;
    }

    public string? GetCommandStateFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 2) is not null and string)
        {
            return (string)_memoryCach.Get(chatId + 2);
        }

        else return null;
    }

    public DatingUserDto GetDatingUserDtoFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 1) is not null and DatingUserDto)
        {
            return (DatingUserDto)_memoryCach.Get(chatId + 1);
        }

        else throw new MemoryCachException();
    }

    public int GetMessageIdFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 3) is not null and int)
        {
            return (int)_memoryCach.Get(chatId + 3);
        }

        else throw new MemoryCachException();
    }

    public void SetMemoryCach(long chatId, DatingUserDto datingUserDto)
    {
        _memoryCach.Set(chatId + 1, datingUserDto,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }

    public void SetMemoryCach(long chatId, string commandState, int messageId)
    {
        _memoryCach.Set(chatId + 2, commandState,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });

        _memoryCach.Set(chatId + 3, messageId,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            });
    }
}
