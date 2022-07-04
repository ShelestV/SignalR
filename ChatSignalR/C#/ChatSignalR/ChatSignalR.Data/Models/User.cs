namespace ChatSignalR.Data.Models;

public sealed class User : ModelBase
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

    public ICollection<Message> MessageForwardedByNavigations { get; set; }
    public ICollection<Message> MessageUsers { get; set; }

    public ICollection<Chat> Chats { get; set; }
    public ICollection<User> Friends { get; set; }
    public ICollection<User> Users { get; set; }
}
