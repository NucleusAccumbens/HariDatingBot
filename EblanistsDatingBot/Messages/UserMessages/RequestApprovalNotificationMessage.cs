namespace EblanistsDatingBot.Messages.UserMessages;

public class RequestApprovalNotificationMessage : BaseMessage
{
    private readonly string _username;

    public RequestApprovalNotificationMessage(string username)
    {
        _username = username;
    }

    public override string MessageText => "<b>chat request approved</b>\n\n" +
        "i <b>don't recommend</b> saying hello, being polite and starting with questions: \n\n" +
        "👎🏿 what's your name?\n" +
        "👎🏿 how old are you?\n" +
        "👎🏿 are you a male or a female?\n" +
        "👎🏿 where are you from?\n" +
        "👎🏿 how are you?\n" +
        "👎🏿 what are you doing?\n\n" +
        "i <b>recommend</b> writing something about sex and sending photos of your genitals. " +
        "your interlocutor received the same recommendation";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithUrl(text: "🔘 send a message", url: $"http://t.me/{_username}")
        },
    });
}
