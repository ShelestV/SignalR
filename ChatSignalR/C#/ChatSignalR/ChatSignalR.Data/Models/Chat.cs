namespace ChatSignalR.Data.Models;

public sealed class Chat : ModelBase
{
    public Chat()
    {
        Messages = new HashSet<Message>();
        Users = new HashSet<User>();
    }

    public string Name { get; set; } = null!;

    public ICollection<Message> Messages { get; set; }

    public ICollection<User> Users { get; set; }
}
