namespace ChatSignalR.BusinessLogic;

public class BeforeResult
{
    public bool Result { get; }
    public Exception? Error { get; }

    /// <summary>
    /// <para>BeforeResult is returned type</para>
    /// <para>If error has been not provided result will be positive</para>
    /// </summary>
    /// <param name="error">Exception that runtime throws</param>
    public BeforeResult(Exception? error = null)
    {
        this.Result = error is null;
        this.Error = error;
    }
}
