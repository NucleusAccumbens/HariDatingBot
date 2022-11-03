namespace EblanistsDatingBot.Common.Interfaces;

public interface IMemoryCachService
{
    void SetMemoryCach(long chatId, DatingUserDto datingUserDto);

    void SetMemoryCach(long chatId, string commandState, int messageId);

    void SetMemoryCach(long chatId, List<DatingUserDto> datingUserDtos);

    void SetMemoryCach(long chatId, List<Photo> photoDtos);

    void SetMemoryCach(long chatId, long requestChatId);

    void SetInterlocutorChatIdInMemoryCach(long chatId, long interlocutorChatId);

    bool SetRequestCountInMemoryCach(long chatId);

    public void SetChatingState(long chatId, long interlocutorChatId);

    DatingUserDto GetDatingUserDtoFromMemoryCach(long chatId);

    string? GetCommandStateFromMemoryCach(long chatId);

    int GetMessageIdFromMemoryCach(long chatId);

    List<DatingUserDto> GetAllUserFromMemoryCach(long chatId);

    DatingUserDto GetCurrentUserFromMemoryCach(long chatId);

    Photo GetCurrentPhotoFromMemoryCach(long chatId);

    Photo GetCurrentPhotoToGoBackFromMemoryCach(long chatId);

    List<long>? GetChatIdOfCompletedRequestsFromMemoryCach(long chatId);

    long GetInterlocutorChatIdFromMemoryCach(long chatId);

    string? GetChatingState(long chatId);


}
