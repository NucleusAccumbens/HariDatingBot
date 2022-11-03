using EblanistsDatingBot.Common.StringBuilders;

namespace EblanistsDatingBot.Messages.UserMessages;

public class StartDatingMessage : BaseMessage
{
    private readonly ProfileStringBuilder _stringBuilder;

    private readonly DatingUserDto _userDto;

    private readonly long _chatId;

    public StartDatingMessage(DatingUserDto datingUserDto, long chatId)
    {
        _userDto = datingUserDto;
        _stringBuilder = new(_userDto);
        _chatId = chatId;
    }

    public override string MessageText => 
        $"person number {_userDto.Id}\n\n<b>if this person approves your request, i'll link your usernames</b>" +
        $"\n\n{_stringBuilder.GetProfileInfo()}";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup(_chatId);

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long chatId)
    {

        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 view photos  ", callbackData: $"wViewPhotos{chatId}"),
                InlineKeyboardButton.WithCallbackData(text: "🔘 next", callbackData: $"wNext")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 request a chat", callbackData: $"wRequestAChat{chatId}")
            },
        });
    }
}
