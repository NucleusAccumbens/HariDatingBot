using Domain.Enums;

namespace EblanistsDatingBot.Models;

public class PhotoDto
{
    public long? Id { get; set; }
    public BodyParts BodyPart { get; set; }
    public string PathToPhoto { get; set; } = string.Empty;
    public long ChatId { get; set; }
}
