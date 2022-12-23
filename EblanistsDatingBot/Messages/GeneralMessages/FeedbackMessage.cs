namespace EblanistsDatingBot.Messages.GeneralMessages;

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
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "we are eblanists", url: $"https://eblanism.bsite.net")
        },
    });
}
