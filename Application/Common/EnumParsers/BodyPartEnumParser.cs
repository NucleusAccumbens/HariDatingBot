using Domain.Enums;

namespace Application.Common.EnumParsers;

public class BodyPartEnumParser
{
    public static string GetBodyPartStringValue(BodyParts bodyPart)
    {
        if (bodyPart == BodyParts.FullBodyFront) return "в полный рост спереди";
        if (bodyPart == BodyParts.FullBodyBack) return "в полный рост сзади";
        if (bodyPart == BodyParts.FullBodySide) return "в полный рост сбоку";
        if (bodyPart == BodyParts.Palm) return "ладони";
        if (bodyPart == BodyParts.PalmBack) return "тыльная сторона ладони";
        if (bodyPart == BodyParts.Feet) return "стопы";
        if (bodyPart == BodyParts.FeetOnTop) return "стопы сверху";
        else return "портрет";
    }
}
