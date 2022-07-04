using ChatSignalR.Core;
using ChatSignalR.Data.Models;
using ChatSignalR.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace ChatSignalR.BusinessLogic;

public sealed class ChatBusinessLogic :
    BusinessLogicBase<Chat>, 
    IChatBusinessLogic
{
    private readonly IChatRepository chatRepository;

    public ChatBusinessLogic(IChatRepository repository, ILogger<BusinessLogicBase<Chat>>? logger = null)
        : base(repository, logger)
    {
        this.chatRepository = repository;
    }

    public async Task<OperationResult<Chat>> GetByNameAsync(string name)
    {
        return await this.chatRepository.GetByNameAsync(name);
    }

    public async Task<OperationResult<bool>> AddUserAsync(Guid chatId, Guid userId)
    {
        return await this.chatRepository.AddUserAsync(chatId, userId);
    }
}
