namespace EblanistsDatingBot.Messages.UserMessages;

public class UserEthicsMessage : BaseMessage
{
    private readonly string _messageText;

    private readonly char _callbackCode;

    public UserEthicsMessage(string messageText, char callbackCode)
    {
        _messageText = messageText;
        _callbackCode = callbackCode;
    }

    public override string MessageText => _messageText;

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 yes", callbackData: $"{_callbackCode}Yes"),
                InlineKeyboardButton.WithCallbackData(text: "🔘 no", callbackData: $"{_callbackCode}No"),
                InlineKeyboardButton.WithCallbackData(text: "💡", callbackData: $"{_callbackCode}Info"),
            },
        });
    }
}
