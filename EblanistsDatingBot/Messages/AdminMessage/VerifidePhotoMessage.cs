using Application.Common.EnumParsers;
using Domain.Enums;

namespace EblanistsDatingBot.Messages.AdminMessage;

public class VerifidePhotoMessage : BaseMessage
{
    private readonly long _photoId;
    
    private readonly BodyParts _bodyPart;

    public VerifidePhotoMessage(BodyParts bodyPart, long id)
    {
        _bodyPart = bodyPart;
        _photoId = id;
    }

    public override string MessageText => 
        $"соответствует ли фото выбранному типу и формату: " +
        $"<b>{BodyPartEnumParser.GetBodyPartStringValue(_bodyPart)}</b>?";

    public override InlineKeyboardMarkup InlineKeyboardMarkup => GetInlineKeyboardMarkup();

    private InlineKeyboardMarkup GetInlineKeyboardMarkup()
    {
        return new(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "🔘 да", callbackData: $"tYes{_photoId}"),
                InlineKeyboardButton.WithCallbackData(text: "🔘 нет", callbackData: $"tNo{_photoId}")
            },
        });
    }
}
