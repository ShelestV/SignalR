namespace ChatSignalR.Core;

public interface IAddable<in T>
{
    System.Threading.Tasks.Task<OperationResult<Guid>> AddAsync(T model);
}
