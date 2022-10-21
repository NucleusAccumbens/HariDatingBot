namespace EblanistsDatingBot.Messages.UserMessages;

public class PhotoMessage : BaseMessage
{
    public override string MessageText => 
        "🔸 i want to show people as they are, so i strictly defined the photo format\n\n" +
        "🔸 i focus on those parts of the body that usually go unnoticed, " +
        "and also invite you to show yourself in full growth from different angles\n\n" +
        "🔸 a portrait photo is best taken without makeup, but this is only a recommendation, not a rule\n\n" +
        "🔸 photos should be of good quality: not too dark and not overexposed\n\n" +
        "🔸 you can delete photos\n\n" + 
        "🔸 nude photos allowed\n\n" +
        "click on the button to choose which photo you are going to upload. " +
        "after uploading, the photos will be sent for verification. " +
        "if it is passed, the photos will appear on your profile. " +
        "you can view sample images by clicking on the <b>\"view samples\"</b> button";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 full body side", callbackData: $"sFullBodySide"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 portrait", callbackData: $"sPortrait")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 full body front", callbackData: $"sFullBodyFront"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 palm back", callbackData: $"sPalmBack"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 full body back", callbackData: $"sFullBodyBack"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 palms", callbackData: $"sPalms"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 feet on top", callbackData: $"sFeetOnTop"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 feet", callbackData: $"sFeet"),
        },
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "🔘 view samples", url: 
                $"https://drive.google.com/drive/folders/1zHIKCsGx-z1sqvcMUwxDZ1ztASKE0R3t?usp=sharing")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"p")
        },
    });
}
