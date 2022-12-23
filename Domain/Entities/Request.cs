namespace Domain.Entities;

public class Request : BaseEntity
{
    public long ChatId { get; set; }

    public long DatingUserId { get; set; }

    public bool IsBlocked { get; set; } = false;

    public DatingUser? DatingUser { get; set; }
}
