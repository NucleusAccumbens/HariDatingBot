namespace EblanistsDatingBot.Messages.UserMessages;

public class DatingMessage : BaseMessage
{
    private readonly bool _isTextCommand;

    public DatingMessage(bool isTextCommand)
    {
        _isTextCommand = isTextCommand;
    }

    public override string MessageText =>
        "🔸 you can send up to 12 dialogue requests per day\n\n" +
        "🔸 you can chat with only one user at a time\n\n" +
        "🔸 you can block unwanted interlocutors\n\n" +
        "🔸 after passing the test for knowledge of STIs " +
        "you will have access to the search by parameters\n\n" +
        "to start viewing profiles, click <b>\"start dating\"</b>";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        if (_isTextCommand == true)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 start dating", callbackData: $"wStartDating"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 pass the test", callbackData: $"qStiTest"),
                },
            });
        }

        else return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 start dating", callbackData: $"wStartDating"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 pass the test", callbackData: $"qStiTest"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"_")
            },
        });
    }
}
