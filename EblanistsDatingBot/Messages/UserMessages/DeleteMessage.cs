namespace EblanistsDatingBot.Messages.UserMessages;

public class DeleteMessage : BaseMessage
{
    public override string MessageText => "are you sure you want to delete the profile?";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "❌ delete profile", callbackData: $"❌")
        },
    });
}
