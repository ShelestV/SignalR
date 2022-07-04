namespace ChatSignalR.BusinessLogic;

public interface IChatBusinessLogic :
    ICrudBusinessLogic<Data.Models.Chat>
{
    Task<Core.OperationResult<Data.Models.Chat>> GetByNameAsync(string name);
    Task<Core.OperationResult<bool>> AddUserAsync(Guid chatId, Guid userId);
}
