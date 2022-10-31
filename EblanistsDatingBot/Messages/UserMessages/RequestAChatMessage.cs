using EblanistsDatingBot.Common.StringBuilders;

namespace EblanistsDatingBot.Messages.UserMessages;

public class RequestAChatMessage : BaseMessage
{
    private readonly ProfileStringBuilder _stringBuilder;

    private readonly DatingUserDto _userDto;

    private readonly long _chatId;

    private readonly bool _isBlocked;

    public RequestAChatMessage(DatingUserDto datingUserDto, long chatId, bool IsBlocked)
    {
        _userDto = datingUserDto;
        _stringBuilder = new(_userDto);
        _chatId = chatId;
        _isBlocked = IsBlocked;
    }

    public override string MessageText =>
        $"<b>this person wants to start a dialogue with you. " +
        $"you can only chat with one person at a time.\n" +
        $"you can go to the dialogue at any time, " +
        $"the application is saved. to view the application, " +
        $"use the command  /requests</b>\n\n" +
        $"{_stringBuilder.GetProfileInfo()}";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup(_chatId);

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
                    InlineKeyboardButton.WithCallbackData(text: "🔘 send a message", callbackData: $"ySendAMessage{chatId}")
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
                InlineKeyboardButton.WithCallbackData(text: "🔘 send a message", callbackData: $"ySendAMessage{chatId}")
            },
        });
    }
}
