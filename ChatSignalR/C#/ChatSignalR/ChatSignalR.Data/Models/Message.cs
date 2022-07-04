namespace ChatSignalR.Data.Models;

public sealed class Message : ModelBase
{
    public string Text { get; set; } = null!;
    public DateTime? SendedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public Guid? ForwardedBy { get; set; }

    public Chat Chat { get; set; } = null!;
    public User? ForwardedByNavigation { get; set; }
    public User User { get; set; } = null!;
}
