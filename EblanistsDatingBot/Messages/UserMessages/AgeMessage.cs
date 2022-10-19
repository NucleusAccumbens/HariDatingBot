namespace EblanistsDatingBot.Messages.UserMessages;

public class AgeMessage : BaseMessage
{
    public override string MessageText => "are you 18 or older?";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 yes", callbackData: $"!Yes"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 no", callbackData: $"!No")
        },
    });
}