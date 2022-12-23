namespace EblanistsDatingBot.Messages.GeneralMessages;

public class RegisterMessage : BaseMessage
{
    public override string MessageText =>
        "🔸 to register, you need to answer 12 questions\n\n" +
        "🔸 the answers will be in your public profile";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 register", callbackData: ".Register")
        },
    });
}
