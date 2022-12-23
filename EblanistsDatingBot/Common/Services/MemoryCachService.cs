using Microsoft.Extensions.Caching.Memory;

namespace EblanistsDatingBot.Common.Services;

public class MemoryCachService : IMemoryCachService
{
    private readonly IMemoryCache _memoryCach;

    public MemoryCachService(IMemoryCache memoryCache)
    {
        _memoryCach = memoryCache;
    }

    public List<DatingUserDto> GetAllUserFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 4) is not null and List<DatingUserDto>)
        {
            return (List<DatingUserDto>)_memoryCach.Get(chatId + 4);
        }

        else throw new MemoryCachException();
    }

    public string? GetCommandStateFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 2) is not null and string)
        {
            return (string)_memoryCach.Get(chatId + 2);
        }

        else return null;
    }

    public Photo GetCurrentPhotoFromMemoryCach(long chatId)
    {
        int currentPhotoIndex = 0;

        if (_memoryCach.Get(chatId + 6) is not null and int)
        {
            int photoIndex = (int)_memoryCach.Get(chatId + 6);

            if (photoIndex != 0)
            {
                currentPhotoIndex = photoIndex - 1;

                SetCurrentPhotoIndex(chatId, photoIndex - 1);
            }
            if (photoIndex == 0)
            {
                currentPhotoIndex = (int)_memoryCach.Get(chatId + 7);

                SetCurrentPhotoIndex(chatId, currentPhotoIndex);
            }
        }

        else throw new MemoryCachException();

        if (_memoryCach.Get(chatId + 5) is not null and List<Photo>)
        {
            return (_memoryCach.Get(chatId + 5) as List<Photo>)[currentPhotoIndex];
        }

        else throw new MemoryCachException();
    }

    public Photo GetCurrentPhotoToGoBackFromMemoryCach(long chatId)
    {
        if (_memoryCach.Get(chatId + 6) is not null and int)
        {
            int currentPhotoIndex = (int)_memoryCach.Get(chatId + 6);

            if (currentPhotoIndex == (int)_memoryCach.Get(chatId + 7))
            {
                currentPhotoIndex = 0;

                SetCurrentPhotoIndex(chatId, currentPhotoIndex);

            }
            else 
            {
                currentPhotoIndex += 1;

                SetCurrentPhotoIndex(chatId, currentPhotoIndex);
            }

            if (_memoryCach.Get(chatId + 5) is not null and List<Photo>)
            {
                return (_memoryCach.Get(chatId + 5) as List<Photo>)[currentPhotoIndex];
            }

            else throw new MemoryCachException();
        }

        else throw new MemoryCachException();       
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

    public DatingUserDto GetCurrentUserFromMemoryCach(long chatId)
    {
        int currentUserIndex = 0;

        if (_memoryCach.Get(chatId + 8) is not null and int)
        {
            int userIndex = (int)_memoryCach.Get(chatId + 8);

            if (userIndex != 0)
            {
                currentUserIndex = userIndex - 1;

                SetCurrentUserIndex(chatId, userIndex - 1);
            }
            if (userIndex == 0)
            {
                currentUserIndex = (int)_memoryCach.Get(chatId + 9);

                SetCurrentUserIndex(chatId, currentUserIndex);
            }
        }

        else throw new MemoryCachException();

        if (_memoryCach.Get(chatId + 4) is not null and List<DatingUserDto>)
        {
            return (_memoryCach.Get(chatId + 4) as List<DatingUserDto>)[currentUserIndex];
        }

        else throw new MemoryCachException();
    }

    public List<long>? GetChatIdOfCompletedRequestsFromMemoryCach(long chatId)
    {

        if (_memoryCach.Get(chatId + 10) is not null and List<long>)
        {
            return (List<long>)_memoryCach.Get(chatId + 10);
        }

        else return null;
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

    public void SetMemoryCach(long chatId, List<DatingUserDto> datingUserDtos)
    {
        _memoryCach.Set(chatId + 4, datingUserDtos,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

        _memoryCach.Set(chatId + 8, datingUserDtos.Count - 1,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

        _memoryCach.Set(chatId + 9, datingUserDtos.Count - 1,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
    }

    public void SetMemoryCach(long chatId, List<Photo> photoDtos)
    {
        _memoryCach.Set(chatId + 5, photoDtos,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

        _memoryCach.Set(chatId + 6, photoDtos.Count - 1,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });

        _memoryCach.Set(chatId + 7, photoDtos.Count - 1,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
    }

    public void SetMemoryCach(long chatId, long requestChatId)
    {
        if (_memoryCach.Get(chatId + 10) is null)
        {
            List<long> chatIdOfCompletedRequests = new();

            chatIdOfCompletedRequests.Add(requestChatId);

            _memoryCach.Set(chatId + 10, chatIdOfCompletedRequests,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });
        }

        else if (_memoryCach.Get(chatId + 10) is not null and List<long>)
        {
            var chatIdOfCompletedRequests = (List<long>)_memoryCach.Get(chatId + 10);

            chatIdOfCompletedRequests.Add(requestChatId);

            _memoryCach.Set(chatId + 10, chatIdOfCompletedRequests,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });
        }
    }

    public bool SetRequestCountInMemoryCach(long chatId)
    {
        if ((int?)_memoryCach.Get(chatId + 11) is not null and int)
        {
            int? count = (int?)_memoryCach.Get(chatId + 11);

            if (count >= 12) return false;

            else
            {
                _memoryCach.Set(chatId + 11, count += 1,
                    new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                    });


                Console.WriteLine($"Пользователь №{chatId} отправил {count}-й по счёту запрос\n\n");

                return true;
            }
        }

        else _memoryCach.Set(chatId + 11, 1,
                new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
                });

        Console.WriteLine($"\nПользователь №{chatId} отправил 1-й по счёту запрос\n");

        return true;       
    }

    private void SetCurrentPhotoIndex(long chatId, int count)
    {
        _memoryCach.Set(chatId + 6, count,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
    }

    private void SetCurrentUserIndex(long chatId, int count)
    {
        _memoryCach.Set(chatId + 8, count,
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            });
    }

}
