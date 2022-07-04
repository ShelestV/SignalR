namespace ChatSignalR.Data.Models;

public class User : ModelBase
{
    public User()
    {
        MessageForwardedByNavigations = new HashSet<Message>();
        MessageUsers = new HashSet<Message>();
        Chats = new HashSet<Chat>();
        Friends = new HashSet<User>();
        Users = new HashSet<User>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<Message> MessageForwardedByNavigations { get; set; }
    public virtual ICollection<Message> MessageUsers { get; set; }

    public virtual ICollection<Chat> Chats { get; set; }
    public virtual ICollection<User> Friends { get; set; }
    public virtual ICollection<User> Users { get; set; }
}
