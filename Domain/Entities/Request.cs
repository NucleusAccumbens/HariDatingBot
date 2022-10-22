namespace Domain.Entities;

public class Request : BaseEntity
{
    public long ChatId { get; set; }

    public long UserId { get; set; }

    public DatingUser? DatingUser { get; set; }
}
