namespace ChatSignalR.Data.Models;

public class Message : ModelBase
{
    public string Text { get; set; } = null!;
    public DateTime? SendedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public Guid? ForwardedBy { get; set; }

    public virtual Chat Chat { get; set; } = null!;
    public virtual User? ForwardedByNavigation { get; set; }
    public virtual User User { get; set; } = null!;
}
