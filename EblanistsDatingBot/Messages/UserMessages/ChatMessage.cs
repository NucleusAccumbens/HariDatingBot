using Telegram.Bot.Types;

namespace EblanistsDatingBot.Messages.UserMessages;

public class ChatMessage : BaseMessage
{
    private readonly long _interlocutorСhatId;

    public ChatMessage(long interlocutorСhatId)
    {
        _interlocutorСhatId = interlocutorСhatId;
    }

    public override string MessageText => "you can start a dialogue. send a message to this chat";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        return new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"yGoBack{_interlocutorСhatId}")
        },
    });
    }
}
