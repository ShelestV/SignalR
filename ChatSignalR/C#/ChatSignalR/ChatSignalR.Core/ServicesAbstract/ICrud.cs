namespace ChatSignalR.Core;

public interface ICrud<TModel> :
    IAddable<TModel>,
    IDeletable,
    IGettable<TModel>,
    IUpdatable<TModel>
{
}
