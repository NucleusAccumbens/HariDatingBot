using EblanistsDatingBot.Common.StringBuilders;

namespace EblanistsDatingBot.Messages.UserMessages;

public class ChatRequestMessage : BaseMessage
{
    private readonly ProfileStringBuilder _stringBuilder;

    private readonly DatingUserDto _userDto;

    private readonly bool _isBlocked;

    public ChatRequestMessage(DatingUserDto datingUserDto, bool IsBlocked)
    {
        _userDto = datingUserDto;
        _stringBuilder = new(_userDto);
        _isBlocked = IsBlocked;
    }

    public override string MessageText =>
        $"<b>this person wants to start a dialogue with you. " +
        $"you can go to the dialogue at any time, the request is saved. " +
        $"to view all requests, use the /requests command. " +
        $"if you accept the request, i'll link your usernames</b>\n\n" +
        $"{_stringBuilder.GetProfileInfo()}";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup(_userDto.ChatId);

    private InlineKeyboardMarkup GetInlineKeyboardMarkup(long chatId)
    {
        if (!_isBlocked)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 view photos  ", callbackData: $"wViewPhotos{chatId}"),
                    InlineKeyboardButton.WithCallbackData(text: "🔘 block", callbackData: $"yBlock{chatId}")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 accept request", callbackData: $"yAccept{chatId}")
                },
            });
        }
        else return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 view photos  ", callbackData: $"wViewPhotos{chatId}"),
                InlineKeyboardButton.WithCallbackData(text: "🔘 unlock", callbackData: $"yUnlock{chatId}")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 accept request", callbackData: $"yAccept{chatId}")
            },
        });
    }
}
