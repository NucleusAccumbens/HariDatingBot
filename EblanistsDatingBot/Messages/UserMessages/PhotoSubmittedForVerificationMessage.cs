namespace EblanistsDatingBot.Messages.UserMessages;

public class PhotoSubmittedForVerificationMessage : BaseMessage
{
    public override string MessageText => "the photo has been sent for review. " +
        "when the photo is reviewed by the moderator, " +
        "you will receive a notification";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 add photos", callbackData: $"rAddPhotos"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 profile", callbackData: $"oProfile")
        },
    });
}
