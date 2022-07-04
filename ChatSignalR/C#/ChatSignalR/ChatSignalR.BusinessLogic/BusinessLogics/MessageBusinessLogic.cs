using ChatSignalR.Data.Models;
using ChatSignalR.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.BusinessLogic.BusinessLogics;

public sealed class MessageBusinessLogic :
    BusinessLogicBase<Message>, 
    IMessageBusinessLogic
{
    public MessageBusinessLogic(IMessageRepository repository, ILogger<BusinessLogicBase<Message>>? logger = null)
        : base(repository, logger)
    {
    }
}
