namespace EblanistsDatingBot.Messages.UserMessages;

public class EditProfileMessage : BaseMessage
{
    public override string MessageText =>
        "choose an action";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 add about", callbackData: $"rAddAbout"),
        },
        new[]
        { 
            InlineKeyboardButton.WithCallbackData(text: "🔘 add photos", callbackData: $"rAddPhotos")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 edit answers", callbackData: $"rEditAnswers")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"o")
        },
    });
}
