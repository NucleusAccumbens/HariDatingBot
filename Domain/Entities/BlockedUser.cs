namespace Domain.Entities;

public class BlockedUser : BaseEntity
{
    public long UserChatId { get; set; }
    public long BlockedUserChatId { get; set; }
}
