namespace ChatSignalR.Core;

public interface IUpdatable<TModel>
{
    System.Threading.Tasks.Task<OperationResult<TModel>> UpdateAsync(System.Guid id, TModel model);
}
