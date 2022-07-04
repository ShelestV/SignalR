namespace ChatSignalR.Core;

public interface IGettable<TModel>
{
    System.Threading.Tasks.Task<OperationResult<TModel>> GetAsync(System.Guid id);
    System.Threading.Tasks.Task<OperationResult<IList<TModel>>> GetAllAsync();
}
