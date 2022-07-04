namespace ChatSignalR.Data.Repositories;

public interface IChatRepository : 
    ICrudRepository<Models.Chat>
{
    Task<Core.OperationResult<Models.Chat>> GetByNameAsync(string name);
    Task<Core.OperationResult<bool>> AddUserAsync(Guid chatId, Guid userId);
}
