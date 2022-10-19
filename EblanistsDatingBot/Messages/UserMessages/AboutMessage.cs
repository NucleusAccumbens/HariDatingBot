namespace EblanistsDatingBot.Messages.UserMessages;

public class AboutMessage : BaseMessage
{
    public override string MessageText =>
        "send a message with a story about yourself " +
        "<i>(no more than 500 characters)</i>";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"p")
        },
    });
}
