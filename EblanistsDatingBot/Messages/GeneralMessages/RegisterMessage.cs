namespace EblanistsDatingBot.Messages.GeneralMessages;

public class RegisterMessage : BaseMessage
{
    public override string MessageText =>
        "🔸 i will help you search for persons by non-typical parameters\n\n" +
        "🔸 this search will open when you pass the STI knowledge test\n\n" +
        "🔸 if you'll have an offline meeting, it will make it safer\n\n" +
        "🔸 if you fail the test, you can only text random persons\n\n" +
        "🔸 you can send 12 free dialogue requests per day\n\n" +
        "🔸 you can block people you don't like\n\n" +
        "🔸 to register, you need to answer 12 questions";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 register", callbackData: ".Register")
        },
    });
}
