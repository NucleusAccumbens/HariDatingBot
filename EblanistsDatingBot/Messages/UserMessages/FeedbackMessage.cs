namespace EblanistsDatingBot.Messages.UserMessages;

public class FeedbackMessage : BaseMessage
{
    public override string MessageText =>
        "to share hari dating stories, ask a question or offer updates, click <b>\"feedback 💬\"</b>";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "feedback 💬", url: "http://t.me/noncredist")
        },
    });
}
