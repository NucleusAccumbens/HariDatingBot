namespace EblanistsDatingBot.Messages.UserMessages;

public class AddPhotoMessage : BaseMessage
{
    public override string MessageText =>
        "send a photo to this chat and i will redirect it to the moderator for verification";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"rAddPhotos")
        },
    });
}
