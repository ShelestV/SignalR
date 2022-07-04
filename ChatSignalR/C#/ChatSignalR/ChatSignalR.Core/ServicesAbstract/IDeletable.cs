namespace ChatSignalR.Core;

public interface IDeletable
{
    public System.Threading.Tasks.Task<OperationResult<bool>> DeleteAsync(System.Guid id);
}
