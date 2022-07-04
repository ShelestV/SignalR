using ChatSignalR.Core;
using ChatSignalR.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.Data.Repositories;

public sealed class ChatRepository : RepositoryBase<Chat>, IChatRepository
{
    protected override string ModelName => "chat";
    protected override Func<ChatDbContext, DbSet<Chat>> GetModelSet => ((context) => context.Chats);

    public ChatRepository(IConfiguration configuration, ILogger<ChatRepository>? logger = null)
        : base(configuration, logger)
    {
    }

    public async Task<OperationResult<Chat>> UpdateAsync(Guid id, Chat model)
    {
        return await base.UpdateAsync(id, model, (x, y) => x.Name = y.Name);
    }

    public async Task<OperationResult<Chat>> GetByNameAsync(string name)
    {
        var result = OperationResult<Chat>.Create();
        try
        {
            await using var context = this.Context;
            result.Done(await context.Chats.FirstOrDefaultAsync(x => x.Name == name));
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot get chat by name: {Name}. Error: {Exception}", name, ex.Message);
            result.Error(ex);
        }
        return result;
    }

    public async Task<OperationResult<bool>> AddUserAsync(Guid chatId, Guid userId)
    {
        var result = OperationResult<bool>.Create();
        try
        {
            await using var context = this.Context;
            var chat = await context.Chats.FindAsync(chatId);
            if (chat is null)
            {
                this.logger?.LogError("Cannot add user to chat. Because chat with id: {Id} does not exist", chatId);
                result.NotFound();
                return result;
            }

            var user = await context.Users.FindAsync(userId);
            if (user is null)
            {
                this.logger?.LogError("Cannot add user to chat. Because user with id: {Id} does not exist", userId);
                result.NotFound();
                return result;
            }

            chat.Users.Add(user);
            user.Chats.Add(chat);

            await context.SaveChangesAsync();
            result.Done(true);
        }
        catch (Exception ex)
        {
            this.logger?.LogError("Cannot add user to chat, chat id: {ChatId}, user id: {UserId}. Error: {Exception}", chatId, userId, ex.Message);
            result.Error(ex);
        }
        return result;
    }
}
