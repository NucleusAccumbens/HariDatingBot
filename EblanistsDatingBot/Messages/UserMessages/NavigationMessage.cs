namespace EblanistsDatingBot.Messages;

public class NavigationMessage : BaseMessage
{
    public override string MessageText =>
        "to view profiles of other users, click \n<b>\"dating\"</b>\n\n" +
        "to add more information about yourself, edit your answers, " +
        "add photos or take the test for knowledge of STIs, " +
        "go to <b>\"profile\"</b>";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => new(new[]
    {
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 dating", callbackData: $"nDating"),
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData(text: "🔘 profile", callbackData: $"oProfile")
        },
    });
}
