namespace EblanistsDatingBot.Messages.UserMessages;

public class EditAnswersMessage : BaseMessage
{
    public override string MessageText =>
        "<b>select a question number to change the answer</b>\n\n" +
        "1. are you a vegan?\n" +
        "2. do you believe in anything?\n" +
        "3. are you childfree?\n" +
        "4. are you a cosmopolitan?\n" +
        "5. do you like BDSM?\n" +
        "6. are you put makeup?\n" +
        "7. do you wear heels?\n" +
        "8. do you have any tattoos?\n" +
        "9. do you like existence?\n" +
        "10. do you shave your legs?\n" +
        "11. do you shave your head?\n" +
        "12. do you have anything sacred?";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 1", callbackData: $"d1"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 2", callbackData: $"d2"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 3", callbackData: $"d3"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 4", callbackData: $"d4"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 5", callbackData: $"d5"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 6", callbackData: $"d6"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 7", callbackData: $"d7"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 8", callbackData: $"d8"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 9", callbackData: $"d9"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 10", callbackData: $"d10"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 11", callbackData: $"d11"),
            InlineKeyboardButton.WithCallbackData(text: "🔘 12", callbackData: $"d12"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"p")
        },
    });
}
