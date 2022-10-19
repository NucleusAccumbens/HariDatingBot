namespace Domain.Entities
{
    public class Photo : BaseAuditableEntity
    {
        public BodyParts BodyPart { get; set; }
        public string PathToPhoto { get; set; } = string.Empty;
        public bool IsVerified { get; set; } = false;
        public bool IsAvatar { get; set; } = false;
        public long ChatId { get; set; }
    }
}
