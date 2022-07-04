namespace ChatSignalR.Data.Models;

public class Chat : ModelBase
{
    public Chat()
    {
        Messages = new HashSet<Message>();
        Users = new HashSet<User>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; }

    public virtual ICollection<User> Users { get; set; }
}
