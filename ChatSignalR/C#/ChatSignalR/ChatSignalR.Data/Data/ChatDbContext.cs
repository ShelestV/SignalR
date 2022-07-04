using ChatSignalR.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatSignalR.Data;

public partial class ChatDbContext : DbContext
{
    private readonly string? connectionString;

    public ChatDbContext(string? connectionString = null)
    {
        this.connectionString = connectionString;
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; } = null!;
    public virtual DbSet<Message> Messages { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && this.connectionString is not null)
            optionsBuilder.UseSqlServer(this.connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.ToTable("Chat");

            entity.HasIndex(e => e.Name, "Chat_Name_Unique")
                .IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("Message");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.SendedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("(sysdatetime())");

            entity.Property(e => e.Text)
                .HasMaxLength(900)
                .IsUnicode(false);

            entity.HasOne(d => d.Chat)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_Chat_Fk");

            entity.HasOne(d => d.ForwardedByNavigation)
                .WithMany(p => p.MessageForwardedByNavigations)
                .HasForeignKey(d => d.ForwardedBy)
                .HasConstraintName("Message_User_ForwardedBy_Fk");

            entity.HasOne(d => d.User)
                .WithMany(p => p.MessageUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Message_User_Fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Name, "User_Name_Unique")
                .IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.Chats)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Participation",
                    l => l.HasOne<Chat>().WithMany().HasForeignKey("ChatId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Participation_Chat_Fk"),
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Participation_User_Fk"),
                    j =>
                    {
                        j.HasKey("UserId", "ChatId").HasName("Participation_Pk");

                        j.ToTable("Participation");
                    });

            entity.HasMany(d => d.Friends)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Friend",
                    l => l.HasOne<User>().WithMany().HasForeignKey("FriendId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Friend_User_FriendId_Fk"),
                    r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Friend_User_UserId_Fk"),
                    j =>
                    {
                        j.HasKey("UserId", "FriendId").HasName("Friend_Pk");

                        j.ToTable("Friend");
                    });

            entity.HasMany(d => d.Users)
                .WithMany(p => p.Friends)
                .UsingEntity<Dictionary<string, object>>(
                    "Friend",
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Friend_User_UserId_Fk"),
                    r => r.HasOne<User>().WithMany().HasForeignKey("FriendId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Friend_User_FriendId_Fk"),
                    j =>
                    {
                        j.HasKey("UserId", "FriendId").HasName("Friend_Pk");

                        j.ToTable("Friend");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
