namespace ChatSignalR.Data.Repositories;

public interface IUserRepository : 
    ICrudRepository<Models.User>
{
    Task<Core.OperationResult<Models.User>> GetByNameAsync(string name);
}
