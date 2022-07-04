namespace ChatSignalR.Data.Repositories;

public interface ICrudRepository<TModel> 
    : Core.ICrud<TModel>
    where TModel : Models.ModelBase
{
}
