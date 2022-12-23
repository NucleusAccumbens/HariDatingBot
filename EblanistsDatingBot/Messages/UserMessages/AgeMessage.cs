namespace EblanistsDatingBot.Messages.UserMessages;

public class AgeMessage : BaseMessage
{
    public override string MessageText => "you must be 18 years of age or older to enter. are you 18+?";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 yes", callbackData: $"!Yes"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 no", callbackData: $"!No")
        },
    });
}