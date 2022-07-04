using ChatSignalR.Core;
using ChatSignalR.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.Data.Repositories;

public sealed class MessageRepository : RepositoryBase<Message>, IMessageRepository
{
    protected override string ModelName => "message";
    protected override Func<ChatDbContext, DbSet<Message>> GetModelSet => ((context) => context.Messages);

    public MessageRepository(IConfiguration configuration, ILogger<RepositoryBase<Message>>? logger = null)
        : base(configuration, logger)
    {
    }

    public async Task<OperationResult<Message>> UpdateAsync(Guid id, Message model)
    {
        return await base.UpdateAsync(id, model, (x, y) => { x.Text = y.Text; x.SendedAt = y.SendedAt; });
    }
}
