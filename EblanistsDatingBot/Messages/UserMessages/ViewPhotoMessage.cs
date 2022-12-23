namespace EblanistsDatingBot.Messages.UserMessages;

public class ViewPhotoMessage : BaseMessage
{
    private readonly bool _countIsOne;
    
    private readonly long _photoId;

    public ViewPhotoMessage(long phoroId, bool countIsOne)
    {
        _photoId = phoroId;
        _countIsOne = countIsOne;
    }
    
    public override string MessageText => String.Empty;

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        if (_countIsOne)
        {
            return new(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "✖️ delete photo", callbackData: $"uDelete{_photoId}")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData(text: "🔘 hide", callbackData: $"xHide")
                },
            });
        }
        
        else return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔙 back", callbackData: $"uBack"),
                InlineKeyboardButton.WithCallbackData(text: "next 🔜", callbackData: $"uNext")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "✖️ delete photo", callbackData: $"uDelete{_photoId}")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 hide", callbackData: $"xHide")
            },
        });
    }
}
