using Telegram.Bot.Types;

namespace EblanistsDatingBot.Messages.UserMessages;

public class ChatMessage : BaseMessage
{
    public override string MessageText => "you can start a dialogue. send a message to this chat";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"yGoBack")
        },
    });
}
