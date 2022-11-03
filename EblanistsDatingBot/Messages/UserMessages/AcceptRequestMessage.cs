namespace EblanistsDatingBot.Messages.UserMessages;

public class AcceptRequestMessage : BaseMessage
{
    private readonly string _username;

    private readonly long _chatId;

    public AcceptRequestMessage(string username, long chatId)
    {
        _username = username;
        _chatId = chatId;
    }

    public override string MessageText => "<b>this person received confirmation of the request, " +
        "now this person has the opportunity to write to you</b>\n\n" +
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
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"yBack{_chatId}")
        },
    });
}
