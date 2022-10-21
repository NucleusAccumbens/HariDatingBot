namespace EblanistsDatingBot.Messages.UserMessages;

public class ViewPhotoMessage : BaseMessage
{
    public override string MessageText => String.Empty;

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"uBack"),
            InlineKeyboardButton.WithCallbackData(text: "next 🔜", callbackData: $"uNext")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "✖️ delete photo", callbackData: $"uDelete")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 hide", callbackData: $"xHide")
        },
    });
}
