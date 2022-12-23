namespace EblanistsDatingBot.Messages.UserMessages;

public class PhotoManagementMessage : BaseMessage
{
    public override string MessageText =>
        "to delete photo click <b>\"manage photos\"</b>";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 manage photos", callbackData: $"vManagePhotos")
        },
    });
}
