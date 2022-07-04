using ChatSignalR.Core;
using ChatSignalR.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.Data.Repositories;

public sealed class MessageRepository : 
    RepositoryBase<Message>, 
    IMessageRepository
{
    protected override string ModelName => "message";

    public MessageRepository(IConfiguration configuration, ILogger<RepositoryBase<Message>>? logger = null)
        : base(configuration, logger)
    {
    }

    protected override DbSet<Message> GetModelSet(ChatDbContext context)
    {
        return context.Messages;
    }

    public async Task<OperationResult<Message>> UpdateAsync(Guid id, Message model)
    {
        return await base.UpdateAsync(id, model, (x, y) => { x.Text = y.Text; x.SendedAt = y.SendedAt; });
    }
}
