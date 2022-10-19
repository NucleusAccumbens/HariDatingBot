namespace EblanistsDatingBot.Common.Interfaces;

public interface IMemoryCachService
{
    void SetMemoryCach(long chatId, DatingUserDto datingUserDto);

    void SetMemoryCach(long chatId, string commandState, int messageId);

    DatingUserDto GetDatingUserDtoFromMemoryCach(long chatId);

    string GetCommandStateFromMemoryCach(long chatId);

    int GetMessageIdFromMemoryCach(long chatId);
}
