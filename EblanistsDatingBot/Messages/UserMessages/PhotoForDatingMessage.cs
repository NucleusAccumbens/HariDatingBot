namespace EblanistsDatingBot.Messages.UserMessages;

public class PhotoForDatingMessage : BaseMessage
{
    public override string MessageText => String.Empty;

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"xBack"),
            InlineKeyboardButton.WithCallbackData(text: "next 🔜", callbackData: $"xNext")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 hide photos", callbackData: $"xHide")
        },
    });
}
